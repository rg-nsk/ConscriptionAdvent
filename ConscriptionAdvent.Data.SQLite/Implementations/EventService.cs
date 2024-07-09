using ConscriptionAdvent.Data.SQLite.Abstract;
using ConscriptionAdvent.Data.SQLite.Dto;
using ConscriptionAdvent.Domain.Constants;
using ConscriptionAdvent.Domain.Interfaces;
using System;

namespace ConscriptionAdvent.Data.SQLite.Implementations
{
    public class EventService : IEventService
    {
        private string _hostname;
        private ILogCommand _logCommand;

        public EventService(string hostname, ILogCommand logCommand)
        {
            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new ArgumentNullException(nameof(hostname));
            }
            
            if (logCommand == null)
            {
                throw new ArgumentNullException(nameof(logCommand));
            }

            _hostname = hostname;
            _logCommand = logCommand;
        }

        public void Fire(string message)
        {
            var now = DateTime.Now;

            _logCommand.Insert(new log()
            {
                hostname = _hostname,
                action = message,
                date = now.ToString(DateConstants.EventDateFormat),
                time = now.ToString(DateConstants.EventTimeFormat)
            });
        }
    }
}
