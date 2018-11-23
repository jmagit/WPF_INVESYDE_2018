using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculadoraVM.Test {
    [TestClass]
    public class UnitTest1 {
        private Calculadora.ViewModels.Calculadora c = new Calculadora.ViewModels.Calculadora();
        [TestMethod]
        public void Digitos() {
            c.Inicio.Execute(null);
            c.Digito.Execute("2");
            Assert.AreEqual("2", c.Pantalla);
        }
        [TestMethod]
        public void Suma() {
            c.Inicio.Execute(null);
            c.Pantalla="22";
            c.Operacion.Execute("+");
            c.Pantalla="33";
            c.Operacion.Execute("-");
            Assert.AreEqual("55", c.Pantalla);
        }
        [TestMethod]
        public void Concatena() {
            c.Inicio.Execute(null);
            c.Pantalla = "22";
            c.Operacion.Execute("^");
            c.Pantalla = "33";
            c.Operacion.Execute("-");
            Assert.AreEqual("2233", c.Pantalla);
        }
    }
}
