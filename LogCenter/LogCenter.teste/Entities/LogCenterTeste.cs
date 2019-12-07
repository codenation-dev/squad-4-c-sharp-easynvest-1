using LogCenter.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogCenter.teste
{
    [TestClass]
    public class LogCenterTeste
    {
        [TestMethod()]
        public void VerificarCampoTitle()
        {
            Log log = new Log();

            Assert.AreEqual(log.Title, null, "O titulo do log é nulo assim que criado.");
            string parametro = "Titulo";

            log.Title = parametro;
            Assert.AreEqual(log.Title, parametro, "Deve conter o parametro que foi setado");
        }

        [TestMethod()]
        public void VerificarCampoDescription()
        {
            Log log = new Log();

            Assert.AreEqual(log.Description, null, "A descrição do log é nulo assim que criado.");
            string parametro = "Descrição";

            log.Description = parametro;
            Assert.AreEqual(log.Description, parametro, "Deve conter o parametro setado");

        }

        [TestMethod()]
        public void VerificarCampoOrigin()
        {
            Log log = new Log();

            Assert.AreEqual(log.Origin, null, "O campo origem do log é nulo assim que criado");
            string parametro = "Origin";

            log.Origin = parametro;
            Assert.AreEqual(log.Origin, parametro, "Deve conter o parametro setado");

        }

        [TestMethod()]
        public void VerificarCampoUserId()
        {
            Log log = new Log();

            Assert.AreEqual(log.UserId, 0, "O campo userID do log é 0 assim que criado");
            int parametro = 1;

            log.UserId = parametro;
            Assert.AreEqual(log.UserId, parametro, "Deve conter o parametro setado");
        }
    }
}
