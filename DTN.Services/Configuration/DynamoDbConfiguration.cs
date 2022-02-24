using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTN.Services.Configuration
{
    public class DynamoDbConfiguration
    {
        /// <summary>
		/// The prefix for the table name.
		/// </summary>
		public string TableNamePrefix { get; set; }
    }
}
