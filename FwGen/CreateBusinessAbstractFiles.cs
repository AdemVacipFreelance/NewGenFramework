﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace FwGen
{
	public class CreateBusinessAbstractFiles : GeneratorBase
	{


		protected override void GenerateClassFiles(string path)
		{
			foreach (var type in Types)
			{
				var content = GenerateClassFilesType(type);
				if (!type.FullName.Contains("ComplexType"))
					File.WriteAllText(path + "I" + type.Name + "Service.cs", content, System.Text.Encoding.UTF8);
			}
		}

		private string GenerateClassFilesType(Type type)
		{
			var projectName = Form1.frm.txtProjectName.Text;

			return fmtClassFile
					.Replace("[ClassName]", type.Name)
					.Replace("[ClassToTitleCase]", type.Name.Substring(0, 1).ToLower() + type.Name.Substring(1, type.Name.Length - 1))
					.Replace("[ProjectName]", projectName);

		}



		private const string fmtClassFile = @"
using System.Collections.Generic;
using [ProjectName].Entities.ComplexTypes;
using [ProjectName].Entities.Concrete;

namespace [ProjectName].Business.Abstract
{
		public interface I[ClassName]Service
		{
				List<[ClassName]> GetAll();
				[ClassName] GetById(int [ClassToTitleCase]Id);
				List<[ClassName]> GetBy[ClassName](int [ClassToTitleCase]Id);
				
				[ClassName] Add([ClassName] [ClassToTitleCase]);
				void Update([ClassName] [ClassToTitleCase]);
				void Delete([ClassName] [ClassToTitleCase]);

		}
}";
	}
}
