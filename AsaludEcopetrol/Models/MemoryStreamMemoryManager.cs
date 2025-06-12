using Aspose.Cells;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models
{
    public class MemoryStreamMemoryManager : CustomImplementationFactory
    {
		RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager();

		public override MemoryStream CreateMemoryStream()
		{

			return manager.GetStream("MemoryStreamMemoryManager");
		}

		public override MemoryStream CreateMemoryStream(int capacity)
		{
			return manager.GetStream("MemoryStreamMemoryManager", capacity);
		}
	}
}