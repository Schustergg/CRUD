using Crud.Business.Interfaces;
using Crud.Business.Notifications;
using FluentValidation.Results;


namespace Crud.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notificador;

        protected BaseService(INotifier notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notification(mensagem));
        }
    }
}
