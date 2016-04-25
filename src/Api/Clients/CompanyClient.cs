﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;

namespace Library
{
	public class CompanyClient : Client, IClient<Company, Companies>
	{
        private const String COMPANIES_RESOURCE = "companies";

		public CompanyClient (Authentication authentication)
            : base (INTERCOM_API_BASE_URL, COMPANIES_RESOURCE, authentication)
		{
		}

		public Company Create (Company company)
		{
			ClientResponse<Company> result = null;
			result = Post<Company> (company);
			return result.Result;
		}

		public Company Update (Company company)
		{
			ClientResponse<Company> result = null;
			result = Post<Company> (company);

			return result.Result;
		}

		private Company CreateOrUpdate (Company company)
		{
			if (company == null) {
				throw new ArgumentNullException ("'company' argument is null.");
			}

			ClientResponse<Company> result = null;
			result = Post<Company> (company);
			return result.Result;
		}

		public Company View (Dictionary<String, String> parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("'parameters' argument is null.");
			}

			if (!parameters.Any ()) {
				throw new ArgumentException ("'parameters' argument should include company_id parameter.");
			}

			ClientResponse<Company> result = null;

			result = Get<Company> (parameters: parameters);
			return result.Result;
		}

		public Company View (String id)
		{
			if (String.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("'parameters' argument is null.");
			}

			ClientResponse<Company> result = null;
            result = Get<Company> (resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + id);
			return result.Result;		
		}

		public Company View (Company company)
		{
			if (company == null) {
				throw new ArgumentNullException ("'company' argument is null.");
			}

			Dictionary<String, String> parameters = new Dictionary<string, string> ();
			ClientResponse<Company> result = null;

			if (!String.IsNullOrEmpty (company.id)) {
                result = Delete<Company> (resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + company.id);
			} else if (!String.IsNullOrEmpty (company.name)) {
				parameters.Add (Constants.NAME, company.name);
				result = Delete<Company> (parameters: parameters);
			} else if (!String.IsNullOrEmpty (company.company_id)) {
				parameters.Add (Constants.COMPANY_ID, company.company_id);
				result = Delete<Company> (parameters: parameters);
			} else {
				throw new ArgumentNullException ("you need to provide either 'company.id', 'company.company_id', 'company.email' to view a company.");
			}

			return result.Result;
		}

		public Companies List ()
		{
			ClientResponse<Companies> result = null;
			result = Get<Companies> ();
			return result.Result;
		}

		public Companies List (Dictionary<String, String> parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("'parameters' argument is null.");
			}

			if (!parameters.Any ()) {
				throw new ArgumentException ("'parameters' argument should include company_id parameter.");
			}

			ClientResponse<Companies> result = null;
			result = Get<Companies> (parameters: parameters);
			return result.Result;
		}

		public Companies List (int page = 1, int per_page = 50, OrderBy orderby = OrderBy.Dsc)
		{
			return null;
		}

		public Users ListUsers(Company company)
		{
			if (company == null) {
				throw new ArgumentNullException ("'company' argument is null.");
			}

			if (String.IsNullOrEmpty (company.id)) {
				throw new ArgumentNullException ("you must provied 'company.id'.");
			}

			String resource = company.id + Path.DirectorySeparatorChar + "users";
			ClientResponse<Users> result = null;
            result = Get<Users> (resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + resource);
			return result.Result;
		}

		public Users ListUsers(String id)
		{
			if (String.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("you must provied 'company.id'.");
			}

			String resource = id + Path.DirectorySeparatorChar + "users";
			ClientResponse<Users> result = null;
            result = Get<Users> (resource: COMPANIES_RESOURCE + Path.DirectorySeparatorChar + resource);
			return result.Result;		
		}
	}
}