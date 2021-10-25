using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace TesteContaBancaria.Domain
{
    /// <summary>
    /// Construtor
    /// </summary>
    /// </summary>
    public class Result : Notifiable<Notification>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public bool Success { get { return !Notifications.Any(); } }

        /// <summary>
        /// Construtor
        /// </summary>
        protected Result()
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        protected Result(IReadOnlyCollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public static Result Ok()
        {
            return new Result();
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public static Result Error(Notification notification)
        {
            return new Result(new List<Notification> { notification });
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public static Result Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result(notifications);
        }
    }

    /// <summary>
    /// Result tipado
    /// </summary>
    public class Result<T> : Notifiable<Notification> where T : class
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get { return !Notifications.Any(); } }

        /// <summary>
        /// Object
        /// </summary>
        public T Object { get; }

        private Result(T obj)
        {
            Object = obj;
        }

        private Result(IReadOnlyCollection<Notification> notifications)
        {
            Object = null;
            AddNotifications(notifications);
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <param name="obj"></param>
        public static Result<T> Ok(T obj)
        {
            return new Result<T>(obj);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="notification"></param>
        public static Result<T> Error(Notification notification)
        {
            return new Result<T>(new List<Notification> { notification });
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="notifications"></param>
        public static Result<T> Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result<T>(notifications);
        }
    }
}