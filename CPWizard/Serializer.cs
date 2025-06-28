using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CPWizard
{
	public static class Serializer
	{
		private static Dictionary<Type, XmlSerializer> m_xmlSerializerDictionary = null;

		static Serializer()
		{
			m_xmlSerializerDictionary = new Dictionary<Type, XmlSerializer>();
		}

		public static T Load<T>(string fileName) where T : class, new()
		{
			return Load<T>(fileName, new Type[] { });
		}

		public static T Load<T>(string fileName, Type[] typeArray) where T : class, new()
		{
			T obj = null;

			try
			{
				XmlSerializer xmlSerializer = null;

				if (!m_xmlSerializerDictionary.TryGetValue(typeof(T), out xmlSerializer))
				{
					List<Type> typeList = new List<Type>();
					typeList.Add(typeof(T));
					typeList.AddRange(typeArray);

					xmlSerializer = XmlSerializer.FromTypes(typeList.ToArray())[0];

					m_xmlSerializerDictionary.Add(typeof(T), xmlSerializer);
				}

				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ProhibitDtd = false;

				using (XmlReader xmlReader = XmlReader.Create(fileName, xmlReaderSettings))
					obj = (T)xmlSerializer.Deserialize(xmlReader);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Load", "Serializer", ex.Message, ex.StackTrace);

				obj = new T();
			}

			return obj;
		}

		public static void Save<T>(string fileName, T obj) where T : class, new()
		{
			Save<T>(fileName, obj, new Type[] { });
		}

		public static void Save<T>(string fileName, T obj, Type[] typeArray) where T : class, new()
		{
			try
			{
				XmlSerializer xmlSerializer = null;

				if (!m_xmlSerializerDictionary.TryGetValue(typeof(T), out xmlSerializer))
				{
					List<Type> typeList = new List<Type>();
					typeList.Add(typeof(T));
					typeList.AddRange(typeArray);

					xmlSerializer = XmlSerializer.FromTypes(typeList.ToArray())[0];

					m_xmlSerializerDictionary.Add(typeof(T), xmlSerializer);
				}

				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.Encoding = Encoding.UTF8;
				xmlWriterSettings.Indent = true;
				xmlWriterSettings.NewLineHandling = NewLineHandling.Entitize;

				using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
					xmlSerializer.Serialize(xmlWriter, obj);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Save", "Serializer", ex.Message, ex.StackTrace);
			}
		}
	}
}
