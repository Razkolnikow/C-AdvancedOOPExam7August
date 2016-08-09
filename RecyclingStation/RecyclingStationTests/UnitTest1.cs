using System.Collections.Generic;

namespace RecyclingStationTests
{
    using System;
    using System.Diagnostics;
    using Moq;
    using RecyclingStation.Factories;
    using RecyclingStation.Models.DisposalStrategies;
    using System.Linq;
    using RecyclingStation.Models.Waste;
    using RecyclingStation.WasteDisposal;
    using RecyclingStation.WasteDisposal.Attributes;
    using RecyclingStation.WasteDisposal.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StrategyHolderAddStrategy_ShouldReturnTrue()
        {
            //Arrange 
            IStrategyHolder strategyHolder = new StrategyHolder();
            var garbage = new RecyclableGarbage("garbage", 5, 5);
            var type = garbage.GetType();
            DisposableAttribute disposalAttribute = (DisposableAttribute)type.GetCustomAttributes(true)
                .FirstOrDefault();
            var attributeType = disposalAttribute.GetType();

            //Act
            bool currentValue = strategyHolder.AddStrategy(
                attributeType, new RecyclableDisposalStrategy(new ProcessingDataFactory()));
            bool expectedValue = true;

            //Assert
            Assert.AreEqual(expectedValue, currentValue);
        }

        [TestMethod]
        public void StrategyHolderAddStrategy_ShouldReturnFalse()
        {
            //Arrange
            IStrategyHolder strategyHolder = new StrategyHolder();
            var garbage = new RecyclableGarbage("garbage", 5, 5);
            var type = garbage.GetType();
            DisposableAttribute disposalAttribute = (DisposableAttribute)type.GetCustomAttributes(true)
                .FirstOrDefault();
            var attributeType = disposalAttribute.GetType();

            //Act
            var strategy = new RecyclableDisposalStrategy(new ProcessingDataFactory());
            strategyHolder.AddStrategy(attributeType, strategy);
            bool currentResult = strategyHolder.AddStrategy(attributeType, strategy);
            bool expectedResult = false;

            //Assert
            Assert.AreEqual(currentResult, expectedResult);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void StrategyHolderAddStrategy_ShouldThrow()
        {
            //Arrange
            IStrategyHolder strategyHolder = new StrategyHolder();
            var strategy = new RecyclableDisposalStrategy(new ProcessingDataFactory());
            var attributeType = typeof(ConditionalAttribute);
            
            //Act and Assert
            strategyHolder.AddStrategy(attributeType, strategy);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void StrategyHolderRemoveStrategy_ShouldThrow()
        {
            //Arrange
            IStrategyHolder strategyHolder = new StrategyHolder();
            var strategy = new RecyclableDisposalStrategy(new ProcessingDataFactory());
            var attributeType = typeof(ConditionalAttribute);
            var correctAttributeType = typeof (RecyclableAttribute);

            //Act and Assert
            strategyHolder.AddStrategy(correctAttributeType, strategy);
            strategyHolder.RemoveStrategy(attributeType);
        }

        [TestMethod]
        public void StrategyHolderRemoveStrategy_ShouldReturnTrue()
        {
            //Arrange
            IStrategyHolder strategyHolder = new StrategyHolder();
            var strategy = new RecyclableDisposalStrategy(new ProcessingDataFactory());
            var correctAttributeType = typeof(RecyclableAttribute);

            //Act
            strategyHolder.AddStrategy(correctAttributeType, strategy);
            bool resultFromRemoveMethod = strategyHolder.RemoveStrategy(correctAttributeType);
            bool expectedResult = true;

            //Assert
            Assert.AreEqual(resultFromRemoveMethod, expectedResult);
        }

        [TestMethod]
        public void StrategyHolderRemoveStrategy_ShouldReturnFalse()
        {
            //Arrange
            IStrategyHolder strategyHolder = new StrategyHolder();
            var correctAttributeType = typeof(RecyclableAttribute);

            //Act
            bool resultFromRemoveMethod = strategyHolder.RemoveStrategy(correctAttributeType);
            bool expectedResult = false;

            //Assert
            Assert.AreEqual(expectedResult, resultFromRemoveMethod);
        }

        [TestMethod]
        public void GarbageProcessorProcessWaste_ShoultReturnCorrectProcessingData()
        {
            //Arrange
            var strategyHolder = new StrategyHolder();
            var type = typeof (RecyclableAttribute);
            var garbage = new RecyclableGarbage("tires", 15, 12);
            var strategy = new RecyclableDisposalStrategy(new ProcessingDataFactory());
            
            //Act
            strategyHolder.AddStrategy(type, strategy);
            var garbageProcessor = new GarbageProcessor(strategyHolder);
            var processingData = garbageProcessor.ProcessWaste(garbage);
            var expectedProcessingData = strategy.ProcessGarbage(garbage);

            //Assert
            Assert.AreEqual(processingData.EnergyBalance, expectedProcessingData.EnergyBalance);
            Assert.AreEqual(processingData.CapitalBalance, expectedProcessingData.CapitalBalance);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GarbageProcessorProcessWaste_ShouldThrow()
        {
            //Arrange
            var strategyHolder = new StrategyHolder();
            var type = typeof(RecyclableAttribute);
            var garbage = new RecyclableGarbage("tires", 15, 12);
            var strategy = new RecyclableDisposalStrategy(new ProcessingDataFactory());

            //Act and Assert
            var garbageProcessor = new GarbageProcessor(strategyHolder);
            var processingData = garbageProcessor.ProcessWaste(garbage);
            var expectedProcessingData = strategy.ProcessGarbage(garbage);
        }

        [TestMethod]
        public void GarbageProcessorProcessWaste_ShouldReturnDataWithZeroEnergyBalanceAndZeroCapitalBalance()
        {
            // Arrange
            var mockedStrategy = new Mock<IGarbageDisposalStrategy>();
            var mockedData = new Mock<IProcessingData>();
            mockedData.SetupGet(d => d.CapitalBalance).Returns(0);
            mockedData.SetupGet(d => d.EnergyBalance).Returns(0);
            mockedStrategy.Setup(f => f.ProcessGarbage(It.IsAny<IWaste>())).Returns(mockedData.Object);

            var mockedStrategyHolder = new Mock<IStrategyHolder>();
            Dictionary<Type, IGarbageDisposalStrategy> strategies = new Dictionary<Type, IGarbageDisposalStrategy>()
            {
                {typeof(BurnableAttribute), mockedStrategy.Object }
            };

            mockedStrategyHolder.Setup(s => s.GetDisposalStrategies)
                .Returns((IReadOnlyDictionary<Type, IGarbageDisposalStrategy>)strategies);
            var garbageProcessor = new GarbageProcessor(mockedStrategyHolder.Object);
            var garbage = new BurnableGarbage("tire", 3, 3);

            //Act
            var data = garbageProcessor.ProcessWaste(garbage);

            //Assert
            Assert.AreEqual(data.CapitalBalance, 0);
            Assert.AreEqual(data.EnergyBalance, 0);
        }
    }
}
