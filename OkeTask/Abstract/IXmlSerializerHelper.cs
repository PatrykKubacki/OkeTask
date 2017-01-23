using System;
using System.Collections;
using System.Web;

namespace OkeTask.Abstract
{

	interface IXmlSerializerHelper
	{
		void Create(string fileName, object target);

		IEnumerable GetAll(string fileName);

		object Get(string fileName, Guid id);

		void Insert(string fileName, object target);

		void Import(string fileName, HttpPostedFileBase fileUpload);
	}

}
