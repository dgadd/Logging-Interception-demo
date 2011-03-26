using System;
using System.Linq;
using System.Reflection;

namespace LoggingDemo.Domain
{
    public class PrimaryKeyBase
    {
        private int _id;

        public int Id
        {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }

        public override bool Equals(object obj)
        {
            var other = (PrimaryKeyBase) obj;
            return (other.Id.Equals(this.Id));
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public static bool operator ==(PrimaryKeyBase left, PrimaryKeyBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PrimaryKeyBase left, PrimaryKeyBase right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var propertyDetails = this.GetType().GetProperties().Where(propertyInfo => propertyInfo.Name != "Id").Aggregate("", (current, propertyInfo) => current + (propertyInfo.Name + "=" + propertyInfo.GetValue(this, null) + "|"));

            if (propertyDetails.Length == 0)
                return this.ClassDescriptionPrefix + "none";

            return this.ClassDescriptionPrefix + propertyDetails;
        }

        private string ClassDescriptionPrefix
        {
            get { return string.Format("[type:{0}] Id={1} Properties:", this.GetType().Name, this.Id);  }
        }
    }
}