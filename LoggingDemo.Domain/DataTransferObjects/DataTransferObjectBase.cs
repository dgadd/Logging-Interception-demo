using System;
using System.Linq;
using System.Reflection;

namespace LoggingDemo.Domain
{
    public class DataTransferObjectBase
    {
        public override string ToString()
        {
            var propertyDetails = this.GetType().GetProperties().Aggregate("", (current, propertyInfo) => current + (propertyInfo.Name + "=" + propertyInfo.GetValue(this, null) + "|"));

            if (propertyDetails.Length == 0)
                return this.ClassDescriptionPrefix + "none";

            return this.ClassDescriptionPrefix + propertyDetails;
        }

        private string ClassDescriptionPrefix
        {
            get { return string.Format("[type:{0}] Properties:", this.GetType().Name);  }
        }
    }
}