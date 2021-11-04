using System.Text.RegularExpressions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Entities
{
    public class Worker : Entity
    {
        // 1 заглавная буква и минимум 1 обычная
        private const string NamePattern = @"^[А-Я][а-я]{1,11}$";

        // имя нужно для уведомления в письме о готовности выдачи
        public WorkerName Name { get; private set; }

        // почта для отправки уведомления о готовности выдачи
        public Email Email { get; private set; }

        public Worker(Email email, WorkerName name)
        {
            SetName(name);
            SetEmail(email);
        }

        private void SetEmail(Email value)
        {
            if (!value.Value.Contains('@'))
            {
                throw new EmailNotValidException($"{nameof(value)} is not Email");
            }

            Email = value;
        }

        private void SetName(WorkerName value)
        {
            if (!Regex.IsMatch(value.FirstName, NamePattern) ||
                !Regex.IsMatch(value.SecondName, NamePattern))
            {
                throw new NameNotValidException($"{nameof(value)} is not valid");
            }

            Name = value;
        }
    }
}