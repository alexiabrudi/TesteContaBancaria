using Flunt.Notifications;
using System.Collections.Generic;

namespace TesteContaBancaria.Domain.ApiModel
{
    public class ErroModel
    {
        public List<string> Erros { get; } = new List<string>();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="erro"></param>
        public ErroModel(string erro)
        {
            Erros.Add(erro);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="erros"></param>
        public ErroModel(IEnumerable<string> erros)
        {
            Erros.AddRange(erros);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notifications"></param>
        public ErroModel(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                Erros.Add(notification.Message);
            }
        }
    }
}
