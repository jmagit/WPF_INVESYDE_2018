    using System;
    using System.Windows.Input;

namespace Aplication.Core {
    /// <summary>
    /// DelegateCommand es una versi�n simplificada del ICommand de WPF. Se puede envolver un m�todo propio,
    /// permitiendo as� su ejecuci�n mediante la vinculaci�n como comando a cualquier objeto WPF.
    /// Adicionalmente, DelegateCommand tambi�n admite una propiedad IsEnabled puede utilizar para activar/desactivar 
    /// el comando.
    /// </summary>
    public class DelegateCommand : ICommand {

        /// <summary>
        /// Referencia al m�todo Execute
        /// </summary>
        private Action<object> executeHandler;
        /// <summary>
        /// Referencia al m�todo CanExecute
        /// </summary>
        private Func<object,bool> canExecuteHandler;

        /// <summary>
        /// Mentiene si el comando est� habilitado.
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        /// Constructor que recibe los delegados a los m�todos Execute y CanExecute para implementar el interfaz ICommand.
        /// </summary>
        /// <param name="ExecuteHandler">Delegado al procedimiento que debe ejecutarse cuando se invoque al comado. La firma del m�todo es:
        /// VB: Sub cmd(o as Object)
        /// C#: void cmd(Object o)
        /// </param>
        /// <param name="CanExecuteHandler">Delegado a la funci�n que debe ejecutarse cuando el WPF quiera saber si 
        /// el comando se puede ejecutar para actualizar el UI. El m�todo devuelve true cuando se puede ejecutar. 
        /// La firma del m�todo es:
        /// VB: Function cmd(o as Object) as Boolean
        /// C#: bool cmd(Object o)
        /// </param>
        public DelegateCommand(Action<object> ExecuteHandler, Func<object,bool> CanExecuteHandler) {
            executeHandler = ExecuteHandler;
            canExecuteHandler = CanExecuteHandler;
        }

        /// <summary>
        /// Constructor simplificado que recibe el delegado al m�todo Execute. Para el m�todo CanExecute necesario 
        /// para implementar el interfaz ICommand, utiliza una referencia a su propio m�todo CanExecuteIfIsEnabled. 
        /// El m�todo CanExecuteIfIsEnabled devuelve si se puede ejecutar el comando en funci�n al valor de isEnabled, 
        /// por defecto es true, por lo que se podr�a ejecutar siempre, salvo que se cambie la propiedad IsEnabled. 
        /// Este mecanismo evita tener que crear m�todos repetitivos, cuando no sea necesario comprobar si se puede 
        /// ejecutar, solamente para cumplir con la interfaz.
        /// </summary>
        /// <param name="ExecuteHandler">Delegado al procedimiento que debe ejecutarse cuando se invoque al comado. La firma del m�todo es:
        /// VB: Sub cmd(o as Object)
        /// C#: void cmd(Object o)
        /// </param>
        public DelegateCommand(Action<object> ExecuteHandler):this(ExecuteHandler, CanExecuteIfIsEnabled) {}

        #region ICommand implementation

        /// <summary>
        /// Ejecutar el comando es tan simple como llamar al m�todo que se suministr� en el constructor.
        /// </summary>
        /// <param name="arg"></param>
        public void Execute(object arg = null) {
            this.executeHandler(arg);
        }

        /// <summary>
        /// Dice si se puede ejecutar el comando.
        /// </summary>
        /// <remarks>
        /// Pasarela al m�todo recibido en el constructor
        /// </remarks>
        /// <param name="arg">Valor del atributo CommandParameter en XAML.</param>
        /// <returns></returns>
        public bool CanExecute(object arg = null) {
            isEnabled=this.canExecuteHandler(arg);
            return isEnabled;
        }

        /// <summary>
        /// Este es el evento que la arquitectura de comando de WPF escucha para saber 
        /// cu�ndo debe actualizar el interfaz de usuario y activar/desactivar el comando.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        #endregion

        
        /// <summary>
        /// La visibilidad p�blica de nuestra bandera isEnabled - 
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
        /// Simple propagador de eventos que se asegura de que alguien est� escuchando el evento antes de lanzarlo.
        /// </summary>
        private void OnCanExecuteChanged() {
            if (this.CanExecuteChanged != null) {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

}