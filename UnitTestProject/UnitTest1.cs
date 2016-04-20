﻿using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        private DeviceRepository devRep = DeviceRepository.GetInstance();
        private DataRepository dataRep = DataRepository.GetInstance();

        [TestMethod]
        public void TestDeviceSave()
        {
            Device device = new Device();
            int num = new Random().Next();
            device.Name = "TestDevice" + num;
            device.Working = true;
            devRep.Save(device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(device.Id, another.Id);
            Assert.AreEqual(device.Name, another.Name);
            Assert.AreEqual(device.Working, another.Working);
            Console.WriteLine(device.Id);
        }

        [TestMethod]
        public void TestDeviceUpdate()
        {
            Device device = new Device();
            int num = new Random().Next();
            device.Name = "TestDeviceUpdate" + num;
            device.Working = true;
            devRep.Save(device);

            string newName = "Hello world";
            bool newWorking = false;
            device.Name = newName;
            device.Working = newWorking;
            devRep.Update(device.Id, device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(newName, another.Name);
            Assert.AreEqual(newWorking, another.Working);
        }

        [TestMethod]
        public void TestDataSave()
        {
            Device device = new Device();
            device.Name = "TestDataSave" + new Random().Next();
            device.Working = true;
            devRep.Save(device);

            Data data = new Data();
            data.Name = "TestDataSave1";
            data.Timestamp = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).Seconds;
            data.Value = new Random().Next();
            data.Device = device;
            dataRep.Save(data);

            Data another = dataRep.GetById(data.Id);

            Assert.AreEqual(another.Device.Id, data.Device.Id);

            foreach (var cur in device.Data)
                Assert.AreEqual(another.Id, cur.Id);
        }

        [TestMethod]
        public void TestDataUpdate()
        {
            Device device = new Device();
            device.Name = "TestDataUpdate" + new Random().Next();
            device.Working = true;
            devRep.Save(device);

            Data data = new Data();
            data.Name = "TestDataUpdate";
            data.Timestamp = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).Seconds;
            data.Value = new Random().Next();
            data.Device = device;
            dataRep.Save(data);

            string newName = "newName";
            long newTs = 1234;
            double value = 12345;
            data.Name = newName;
            data.Timestamp = newTs;
            data.Value = value;
            dataRep.Update(data.Id, data);


            Data another = dataRep.GetById(data.Id);
            Assert.AreEqual(newName, another.Name);
            Assert.AreEqual( newTs, another.Timestamp);
            Assert.AreEqual(value, another.Value);
        }





        
    }
}