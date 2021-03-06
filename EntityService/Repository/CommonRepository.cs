﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using EntityService.IRepository;
using NHibernate;
using Entity.Models;

namespace EntityService.Repository
{
    public abstract class CommonRepository<T> : ICommonRepository<T>
        where T : Idable
    {
       


        public T GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (T)session.Get(typeof(T), objectId);
            }
        }

        public abstract ICollection<T> GetAll();

        public int Save(ref T objectToAdd)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {                        
                        int ret = (int)session.Save(objectToAdd);
                        session.Persist(objectToAdd);                        
                        objectToAdd.SetId(ret);
                        
                        session.Flush();
                        session.Refresh(objectToAdd);
                        transaction.Commit();
                        return ret;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }                    
                }
            }
        }

        public int Save(T objectToAdd)
        {
            return Save(ref objectToAdd);
        }

        public T Update(int id, T objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        T obj = GetById(objectToUpdate.GetId());
                        Idable newObj = session.Merge<Idable>(obj);
                        transaction.Commit();
                        return (T)newObj;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Remove(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        T obj = GetById(id);
                        session.Delete(obj);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }
    }
}
