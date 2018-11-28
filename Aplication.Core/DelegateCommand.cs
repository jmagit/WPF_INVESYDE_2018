    using System;
    using System.Windows.Input;

namespace Aplication.Core {
    /// <summary>
    /// DelegateCommand es una versión simplificada del ICommand de WPF. Se puede envolver un método propio,
    /// permitiendo así su ejecución mediante la vinculación como comando a cualquier objeto WPF.
    /// Adicionalmente, DelegateCommand también admite una propiedad IsEnabled puede utilizar para activar/desactivar 
    /// el comando.
    /// </summary>
    public class DelegateCommand : ICommand {

        /// <summary>
        /// Referencia al método Execute
        /// </summary>
        private Action<object> executeHandler;
        /// <summary>
        /// Referencia al método CanExecute
        /// </summary>
        private Func<object,bool> canExecuteHandler;

        /// <summary>
        /// Mentiene si el comando está habilitado.
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        /// Constructor que recibe los delegados a los métodos Execute y CanExecute para implementar el interfaz ICommand.
        /// </summary>
        /// <param name="ExecuteHandler">Delegado al procedimiento que debe ejecutarse cuando se invoque al comado. La firma del método es:
        /// VB: Sub cmd(o as Object)
        /// C#: void cmd(Object o)
        /// </param>
        /// <param name="CanExecuteHandler">Delegado a la función que debe ejecutarse cuando el WPF quiera saber si 
        /// el comando se puede ejecutar para actualizar el UI. El método devuelve true cuando se puede ejecutar. 
        /// La firma del método es:
        /// VB: Function cmd(o as Object) as Boolean
        /// C#: bool cmd(Object o)
        /// </param>
        public DelegateCommand(Action<object> ExecuteHandler, Func<object,bool> CanExecuteHandler) {
            executeHandler = ExecuteHandler;
            canExecuteHandler = CanExecuteHandler;
        }

        /// <summary>
        /// Constructor simplificado que recibe el delegado al método Execute. Para el método CanExecute necesario 
        /// para implementar el interfaz ICommand, utiliza una referencia a su propio método CanExecuteIfIsEnabled. 
        /// El método CanExecuteIfIsEnabled devuelve si se puede ejecutar el comando en función al valor de isEnabled, 
        /// por defecto es true, por lo que se podría ejecutar siempre, salvo que se cambie la propiedad IsEnabled. 
        /// Este mecanismo evita tener que crear métodos repetitivos, cuando no sea necesario comprobar si se puede 
        /// ejecutar, solamente para cumplir con la interfaz.
        /// </summary>
        /// <param name="ExecuteHandler">Delegado al procedimiento que debe ejecutarse cuando se invoque al comado. La firma del método es:
        /// VB: Sub cmd(o as Object)
        /// C#: void cmd(Object o)
        /// </param>
        public DelegateCommand(Action<object> ExecuteHandler):this(ExecuteHandler, CanExecuteIfIsEnabled) {}

        #region ICommand implementation

        /// <summary>
        /// Ejecutar el comando es tan simple como llamar al método que se suministró en el constructor.
        /// </summary>
        /// <param name="arg"></param>
        public void Execute(object arg = null) {
            this.executeHandler(arg);
        }

        /// <summary>
        /// Dice si se puede ejecutar el comando.
        /// </summary>
        /// <remarks>
        /// Pasarela al método recibido en el constructor
        /// </remarks>
        /// <param name="arg">Valor del atributo CommandParameter en XAML.</param>
        /// <returns></returns>
        public bool CanExecute(object arg = null) {
            isEnabled=this.canExecuteHandler(arg);
            return isEnabled;
        }

        /// <summary>
        /// Este es el evento que la arquitectura de comando de WPF escucha para saber 
        /// cuándo debe actualizar el interfaz de usuario y activar/desactivar el comando.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        #endregion

        
        /// <summary>
        /// La visibilidad pública de nuestra bandera isEnabled - 
        /// tenga en cuenta que cuando se establece, tenemos que elevar el caso a WPF
        /// fin de que actualice la interfaz de usuario que utiliza este comando.
        /// </summary>
        public bool IsEnabled {
            get { return this.isEnabled; }
            set {
                this.isEnabled = value;
                this.OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// Permite que se ejecute siempre, no es necesario definir un metodo adicional
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>true</returns>
        static bool CanExecuteIfIsEnabled(object arg) {
            return true;
        }

        /// <summary>
        /// Simple propagador de eventos que se asegura de que alguien está escuchando el evento antes de lanzarlo.
        /// </summary>
        private void OnCanExecuteChanged() {
            if (this.CanExecuteChanged != null) {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

}