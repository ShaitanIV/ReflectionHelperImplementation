using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Infrastructure.Helpers
{
	public static class ReflectionHelper
	{
		public static object GetPropertyValue(object instance, string propertyName)
		{
            if (instance == null)
            {
				throw new NullReferenceException("Object instance cannot be null");
            }

			if (propertyName == null)
			{
				throw new NullReferenceException("Property name cannot be null");
			}

			if (propertyName.Contains('.'))
			{
				var tempArray = propertyName.Split('.', 2);
				return GetPropertyValue(GetPropertyValue(instance, tempArray[0]), tempArray[1]);
			}
			else
			{
				var pathToProperty = instance.GetType().GetProperty(propertyName);
                if (pathToProperty==null)
                {
					throw new NullReferenceException("Such property does not exists");
                }
                else
                {
					return pathToProperty.GetValue(instance);
				}
			}
		}

		public static object GetPropertyValueByType(object instance, string propertyType)
		{
			//вернуть первый из найденых property с заданными типом.
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			if (propertyType == null)
			{
				throw new NullReferenceException("Property type cannot be null");
			}

			var propertiesList = instance.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);

            foreach (var property in propertiesList)
            {
                if (property.PropertyType.Name==propertyType)
                {
					return GetPropertyValue(instance, property.Name);
                }
            }

			return null;
		}

		public static bool HasProperty(object instance, string propertyName)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			if (propertyName == null)
			{
				throw new NullReferenceException("Property name cannot be null");
			}

			if (propertyName.Contains('.'))
			{
				var tempArray = propertyName.Split('.', 2);
				return HasProperty(GetPropertyValue(instance, tempArray[0]), tempArray[1]);
			}
			else
			{
                foreach (var property in instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if (propertyName==property.Name)
                    {
						return true;
                    }
                }
			}
			return false;
		}

		public static object GetPropertyValue(object instance, params string[] propertyNames)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			if (propertyNames == null || propertyNames.Length == 0)
			{
				throw new NullReferenceException("Property names cannot be null");
			}

            foreach (var propertyName in propertyNames)
            {
				var result = GetPropertyValue(instance, propertyName);

                if (result!=null)
                {
					return result;
                }
			}

			return null;
		}

		public static PropertyInfo GetProperty(Type type, string propertyName)
		{
			if (type == null)
			{
				throw new NullReferenceException("Type cannot be null");
			}

			if (propertyName == null)
			{
				throw new NullReferenceException("Property name cannot be null");
			}

			return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

		}

		public static PropertyInfo GetProperty(object instance, string propertyName)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			if (propertyName == null)
			{
				throw new NullReferenceException("Property name cannot be null");
			}
			
			if (propertyName.Contains('.'))
			{
				var tempArray = propertyName.Split('.', 2);
				return GetProperty(GetProperty(instance, tempArray[0]).GetValue(instance), tempArray[1]);
			}
			else
			{
				var pathToProperty = instance.GetType().GetProperty(propertyName);
				return pathToProperty == null ? null : pathToProperty;
			}
		} 

		public static Type GetPropertyType(object instance, string propertyName)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			if (propertyName == null)
			{
				throw new NullReferenceException("Property name cannot be null");
			}

			return GetProperty(instance,propertyName).PropertyType;
		}

		public static List<Attribute> GetCustomAttributes(PropertyInfo property)
		{
			if (property == null)
			{
				throw new NullReferenceException("Property cannot be null");
			}

			var propertyAttirbutesAsObjects = property.GetCustomAttributes(false);
			var propertyAttributes = new List<Attribute>();
            foreach (var propertyAttribureAsObject in propertyAttirbutesAsObjects)
            {
				propertyAttributes.Add((Attribute)propertyAttribureAsObject);
            }

			return propertyAttributes;
		}

		public static List<MethodInfo> GetMethodsInfo(object instance)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			return instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList();
		}

		public static List<FieldInfo> GetFieldsInfo(object instance)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			return instance.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList();
		}

		public static object CallMethod(object instance, string methodName, object[] param)
		{
			if (instance == null)
			{
				throw new NullReferenceException("Object instance cannot be null");
			}

			if (methodName == null)
			{
				throw new NullReferenceException("Method name cannot be null");
			}

			var result = new object();

            try
            {
				result = instance.GetType().GetMethod(methodName).Invoke(instance, param);
			}
			catch(TargetParameterCountException)
            {
				Console.WriteLine("Parameters don't match");
				return null;
            }
			catch
            {
				Console.WriteLine("Unknown error occurred");
				return null; 
			}
            
			return result;
		}

	}
}
