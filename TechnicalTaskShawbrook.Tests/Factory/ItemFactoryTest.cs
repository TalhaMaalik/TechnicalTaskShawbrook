using BusinessLayer.Processors.Factory;
using DataAccessLayer.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTaskShawbrook.Tests.Factory
{
    public class ItemFactoryTest
    {
        private readonly Mock<IItemRepository> _ItemRepositoryMock = new();

        private readonly ItemFactory _Sut;
        public ItemFactoryTest()
        {
            _Sut = new ItemFactory(_ItemRepositoryMock.Object);
        }
    }
}
