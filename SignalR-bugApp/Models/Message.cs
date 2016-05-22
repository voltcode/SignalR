using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SignalR_bugApp.Models
{
    public interface IWrapper
    {
        Guid WrapperId
        {
            get;
        }
    }

    [DataContract]
    public class Wrapper : IWrapper
    {
        public Wrapper( Guid wrapperId)
        {
            this.WrapperId = wrapperId;
        }

        [DataMember]
        public Guid WrapperId { get; private set; }
    }

    [DataContract]
    public class Message
    {
        public Message( Guid id, string name, IWrapper wrapper)
        {
            this.Id = id;
            this.Name = name;
            this.Wrapper = wrapper;
        }

        [DataMember]
        public Guid Id { get; private set; }

        [DataMember(Name="name")]
        public string Name { get; private set; }

        [DataMember]
        public IWrapper Wrapper { get; private set; } 
    }
}