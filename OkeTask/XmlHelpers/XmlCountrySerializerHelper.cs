using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using OkeTask.Abstract;
using OkeTask.Models;

namespace OkeTask.XmlHelpers
{

	public class XmlCountrySerializerHelper : IXmlSerializerHelper
	{
		XmlReader reader;
		Stream stream;
		XmlWriter writer;
		XmlSerializer xmlSerializer;

		public void Create(string fileName, object target)
		{
			xmlSerializer = new XmlSerializer(typeof(List<Country>));
			stream = new FileStream(fileName, FileMode.Create);
			using (writer = new XmlTextWriter(stream, Encoding.Unicode))
				xmlSerializer.Serialize(writer, target);
		}

		public IEnumerable GetAll(string fileName)
		{
			if (!File.Exists(fileName)) return null;

			IList<Country> countries;
			xmlSerializer = new XmlSerializer(typeof(List<Country>));
			stream = new FileStream(fileName, FileMode.Open);
			using (reader = new XmlTextReader(stream))
				countries = (IList<Country>)xmlSerializer.Deserialize(reader);

			return countries;
		}

		public void Insert(string fileName, object target)
		{
			var countries = (List<Country>)GetAll(fileName);
			countries.Add((Country)target);
			Create(fileName, countries);
		}

		public object Get(string fileName, Guid id)
		{
			var countries = GetAll(fileName);
			var country = ((List<Country>)countries).FirstOrDefault(c => c.Id == id);
			return country;
		}

		public void Import(string fileName, HttpPostedFileBase fileUpload)
		{
			if (fileUpload == null || fileUpload.ContentLength <= 0) return;

			IList<Country> countries;
			xmlSerializer = new XmlSerializer(typeof(List<Country>));
			stream = fileUpload.InputStream;
			using (reader = new XmlTextReader(stream))
				countries = (IList<Country>)xmlSerializer.Deserialize(reader);
			Create(fileName, countries);
		}
	}

}
