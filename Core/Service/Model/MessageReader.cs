using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Medibox.Service.Model
{
    internal class MessageReader
    {
        private static readonly MethodInfo _dataToXElement = typeof(MessageReader).GetMethod("DataToXElement");
        private static readonly MethodInfo _dataContractToXElement = typeof(MessageReader).GetMethod("DataContractToXElement");
        private static readonly MethodInfo _readInstance = typeof(MessageReader).GetMethod("ReadInstance");
        private static readonly MethodInfo _addXElement = typeof(MessageReader).GetMethod("AddXElement");

        public static Stream ToMessageStream<T>(T obj, WebMessageFormat format, string callback) where T : new()
        {
            if ((object)obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (format == WebMessageFormat.Json)
            {
                return MessageReader.ToJsonMessageStream<T>(obj, callback);
            }
            return MessageReader.ToXmlMessageStream<T>(obj);
        }

        private static Stream ToXmlMessageStream<T>(T obj) where T : new()
        {
            XElement xelement = !System.Attribute.IsDefined((MemberInfo)typeof(T), typeof(DataContractAttribute)) ? (!System.Attribute.IsDefined((MemberInfo)typeof(T), typeof(CollectionDataContractAttribute)) ? (!typeof(ICollection<>).IsAssignableFrom(obj.GetType().GetGenericTypeDefinition()) ? MessageReader.DataToXElement<T>(obj) : MessageReader.CollectionToXElement<T>(obj)) : MessageReader.CollectionDataContractToXElement<T>(obj)) : MessageReader.DataContractToXElement<T>(obj);
            if (xelement == null)
            {
                return (Stream)null;
            }
            MemoryStream memoryStream = new MemoryStream();
            xelement.Save((Stream)memoryStream);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            return (Stream)memoryStream;
        }

        private static Stream ToJsonMessageStream<T>(T obj, string callback) where T : new()
        {
            string s = JsonConvert.SerializeObject((object)obj, new JsonConverter[1]
              {
                (JsonConverter) new IsoDateTimeConverter()
              });
            if (string.IsNullOrEmpty(s))
            {
                return (Stream)null;
            }
            if (!string.IsNullOrWhiteSpace(callback))
            {
                s = callback + "(" + s + ")";
            }
            return (Stream)new MemoryStream(Encoding.UTF8.GetBytes(s));
        }

        public static XElement DataToXElement<T>(T obj) where T : new()
        {
            if ((object)obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            XElement element = new XElement((XName)typeof(T).Name);
            MessageReader.AddXElement<T>(obj, element, false);
            return element;
        }

        public static XElement DataContractToXElement<T>(T obj) where T : new()
        {
            if ((object)obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string name = typeof(T).Name;
            DataContractAttribute contractAttribute = (DataContractAttribute)typeof(T).GetCustomAttributes(typeof(DataContractAttribute), true)[0];
            if (!string.IsNullOrWhiteSpace(contractAttribute.Name))
            {
                name = contractAttribute.Name;
            }
            XElement element = new XElement((XName)name);
            MessageReader.AddXElement<T>(obj, element, true);
            return element;
        }

        public static XElement CollectionToXElement<T>(T obj) where T : new()
        {
            if ((object)obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            XElement xelement = new XElement((XName)typeof(T).Name);
            PropertyInfo property1 = obj.GetType().GetProperty("Count");
            if (property1 == (PropertyInfo)null)
            {
                return xelement;
            }
            int num = (int)property1.GetGetMethod().Invoke((object)obj, (object[])null);
            if (num <= 0)
            {
                return xelement;
            }
            PropertyInfo property2 = obj.GetType().GetProperty("Item");
            Type propertyType = property2.PropertyType;
            for (int index = 0; index < num; ++index)
            {
                object content = property2.GetGetMethod().Invoke((object)obj, new object[1]
                {
                  (object) index
                });
                if (content != null)
                {
                    if (propertyType.IsValueType || propertyType == typeof(string))
                    {
                        if (propertyType == typeof(DateTime))
                        {
                            content = (object)((DateTime)content).ToString("yyyy-MM-ddTHH:mm:ss.ff");
                        }
                        xelement.Add((object)new XElement((XName)"Item", content));
                    }
                    else
                    {
                        object obj1 = MessageReader._dataToXElement.MakeGenericMethod(propertyType).Invoke((object)null, new object[1]
                            {
                              content
                            });
                        xelement.Add((object)(XElement)obj1);
                    }
                }
            }
            return xelement;
        }

        public static XElement CollectionDataContractToXElement<T>(T obj) where T : new()
        {
            if ((object)obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string name = typeof(T).Name;
            object[] customAttributes = typeof(T).GetCustomAttributes(typeof(CollectionDataContractAttribute), true);
            if (customAttributes != null && customAttributes.Length > 0)
            {
                CollectionDataContractAttribute contractAttribute = (CollectionDataContractAttribute)customAttributes[0];
                if (!string.IsNullOrWhiteSpace(contractAttribute.Name))
                {
                    name = contractAttribute.Name;
                }
            }
            XElement xelement = new XElement((XName)name);
            PropertyInfo property1 = obj.GetType().GetProperty("Count");
            if (property1 == (PropertyInfo)null)
            {
                return xelement;
            }
            int num = (int)property1.GetGetMethod().Invoke((object)obj, (object[])null);
            if (num <= 0)
            {
                return xelement;
            }
            PropertyInfo property2 = obj.GetType().GetProperty("Item");
            Type propertyType = property2.PropertyType;
            for (int index = 0; index < num; ++index)
            {
                object content = property2.GetGetMethod().Invoke((object)obj, new object[1]
                {
                  (object) index
                });
                if (content != null)
                {
                    if (propertyType.IsValueType || propertyType == typeof(string))
                    {
                        if (propertyType == typeof(DateTime))
                        {
                            content = (object)((DateTime)content).ToString("yyyy-MM-ddTHH:mm:ss.ff");
                        }
                        xelement.Add((object)new XElement((XName)"Item", content));
                    }
                    else
                    {
                        object obj1 = MessageReader._dataContractToXElement.MakeGenericMethod(propertyType).Invoke((object)null, new object[1]
                            {
                              content
                            });
                        xelement.Add((object)(XElement)obj1);
                    }
                }
            }
            return xelement;
        }

        public static void AddXElement<T>(T obj, XElement element, bool dataMemberOnly) where T : new()
        {
            if ((object)obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (!dataMemberOnly | System.Attribute.IsDefined((MemberInfo)propertyInfo, typeof(DataMemberAttribute)))
                {
                    string name = propertyInfo.Name;
                    if (dataMemberOnly)
                    {
                        DataMemberAttribute dataMemberAttribute = (DataMemberAttribute)propertyInfo.GetCustomAttributes(typeof(DataMemberAttribute), true)[0];
                        if (!string.IsNullOrWhiteSpace(dataMemberAttribute.Name))
                        {
                            name = dataMemberAttribute.Name;
                        }
                    }
                    XElement xelement = new XElement((XName)name);
                    Type propertyType = propertyInfo.PropertyType;
                    object obj1 = propertyInfo.GetGetMethod().Invoke((object)obj, (object[])null);
                    if (obj1 != null)
                    {
                        if (propertyType == typeof(DateTime))
                        {
                            xelement.Value = ((DateTime)obj1).ToString("yyyy-MM-ddTHH:mm:ss.ff");
                        }
                        else if (propertyType.IsValueType || propertyType == typeof(string))
                        {
                            xelement.Value = obj1.ToString();
                        }
                        else if (System.Attribute.IsDefined((MemberInfo)propertyType, typeof(CollectionDataContractAttribute)))
                        {
                            xelement = MessageReader.CollectionDataContractToXElement<object>(obj1);
                            xelement.Name = (XName)name;
                        }
                        else
                        {
                            MessageReader._addXElement.MakeGenericMethod(propertyType).Invoke((object)null, new object[3]
                              {
                                obj1,
                                (object) xelement,
                                (object) dataMemberOnly
                              });
                        }
                        element.Add((object)xelement);
                    }
                }
            }
        }

        public static IEnumerable<XElement> ReadXElements(XmlDictionaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            XElement xelement = XElement.Load((XmlReader)reader);
            if (xelement == null)
            {
                return Enumerable.Empty<XElement>();
            }
            return xelement.Elements();
        }

        public static MessageBody<T> Read<T>(XmlDictionaryReader reader) where T : new()
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            XElement element = XElement.Load((XmlReader)reader);
            if (element == null)
            {
                return (MessageBody<T>)null;
            }
            MessageBody<T> messageBody = new MessageBody<T>();
            messageBody.Value = MessageReader.ReadInstance<T>(element, messageBody.ReadProperties);
            return messageBody;
        }

        public static PropertyInfo GetProperty<T>(T obj, string propertyName) where T : new()
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return (PropertyInfo)null;
            }
            PropertyInfo propertyInfo1 = obj.GetType().GetProperty(propertyName);
            if (propertyInfo1 == (PropertyInfo)null)
            {
                foreach (PropertyInfo propertyInfo2 in obj.GetType().GetProperties())
                {
                    if (System.Attribute.IsDefined((MemberInfo)propertyInfo2, typeof(DataMemberAttribute)))
                    {
                        foreach (DataMemberAttribute dataMemberAttribute in (DataMemberAttribute[])propertyInfo2.GetCustomAttributes(typeof(DataMemberAttribute), true))
                        {
                            if (!string.IsNullOrWhiteSpace(dataMemberAttribute.Name) && propertyName == dataMemberAttribute.Name)
                            {
                                propertyInfo1 = propertyInfo2;
                                break;
                            }
                        }
                        if (propertyInfo1 != (PropertyInfo)null)
                        {
                            break;
                        }
                    }
                }
            }
            return propertyInfo1;
        }

        public static T ReadInstance<T>(XElement element, CollectionAndValue<PropertyInfo> properties) where T : new()
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            T obj = new T();
            if (System.Attribute.IsDefined((MemberInfo)typeof(T), typeof(CollectionDataContractAttribute)))
            {
                obj.GetType().GetMethod("AddRange");
                List<string> list = new List<string>();
                foreach (XElement xelement in element.Elements())
                {
                    list.Add(xelement.Value);
                }
                obj.GetType().GetMethod("AddRange").Invoke((object)obj, new object[1]
                    {
                      (object) list.ToArray()
                    });
                return obj;
            }
            foreach (XElement xelement in element.Elements())
            {
                PropertyInfo property = MessageReader.GetProperty<T>(obj, xelement.Name.ToString());
                if (property != (PropertyInfo)null)
                {
                    CollectionAndValue<PropertyInfo> collectionAndValue = new CollectionAndValue<PropertyInfo>(property);
                    properties.Add(collectionAndValue);
                    if (!xelement.IsEmpty && !string.IsNullOrEmpty(xelement.Value))
                    {
                        Type propertyType = property.PropertyType;
                        if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                        {
                            if (!string.IsNullOrWhiteSpace(xelement.Value))
                            {
                                CultureInfo cultureInfo = new CultureInfo("en-US");
                                string[] formats = new string[6]
                                {
                                  "yyyy-MM-ddTHH:mm:ss",
                                  "yyyy-MM-ddTHH:mm:ss.ff",
                                  "ddd MMM dd HH:mm:ss +0000 yyyy",
                                  "yyyy/MM/dd HH:mm:ss",
                                  "yyyy/MM/dd HH:mm:ss.ff",
                                  "ddd, dd MMM yyyy HH:mm:ss GMT"
                                };
                                DateTime dateTime = DateTime.ParseExact(xelement.Value, formats, (IFormatProvider)cultureInfo, DateTimeStyles.None);
                                property.SetValue((object)obj, (object)dateTime, (object[])null);
                            }
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            property.SetValue((object)obj, (object)xelement.Value, (object[])null);
                        }
                        else
                        {
                            object result = (object)null;
                            if (propertyType.IsValueType)
                            {
                                if (!String2Extensions.TryChangeType(xelement.Value, property.PropertyType, out result))
                                {
                                    throw new Exception();
                                }
                                property.SetValue((object)obj, result, (object[])null);
                            }
                            else
                            {
                                MethodInfo methodInfo = MessageReader._readInstance.MakeGenericMethod(propertyType);
                                property.SetValue((object)obj, methodInfo.Invoke((object)null, new object[2]
                                {
                                    (object) xelement,
                                    (object) collectionAndValue
                                }), (object[])null);
                            }
                        }
                    }
                }
            }
            return obj;
        }
    }
}
