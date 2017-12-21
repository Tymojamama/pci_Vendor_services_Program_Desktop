﻿using VSP.Business.Components;
using PensionConsultants.Data.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace VSP.Business.Entities
{
    public class AdvisorService : DatabaseEntity
    {
        public Guid PlanAdvisorId;
        public Guid ServiceId;
        public SqlBoolean ServiceOffered;

        private static string _tableName = "AdvisorService";

        public AdvisorService()
            : base(_tableName)
        {

        }

        public AdvisorService(Guid primaryKey)
            : base(_tableName, primaryKey)
        {
            RefreshMembers();
        }

        /// <summary>
        /// Registers the instance's members with the abstract class in order to perform database operations. Do not register members
        /// that exist within the abstract class (e.g. CreatedOn).
        /// </summary>
        protected override void RegisterMembers()
        {
            base.AddColumn("PlanAdvisorId", this.PlanAdvisorId);
            base.AddColumn("ServiceId", this.ServiceId);
            base.AddColumn("ServiceOffered", this.ServiceOffered);
        }

        /// <summary>
        /// Resets the values of all public members to their values in the database.
        /// </summary>
        protected override void SetRegisteredMembers()
        {
            this.PlanAdvisorId = (Guid)base.GetColumn("PlanAdvisorId");
            this.ServiceId = (Guid)base.GetColumn("ServiceId");
            this.ServiceOffered = (SqlBoolean)base.GetColumn("ServiceOffered");
        }

        public static DataTable GetActive()
        {
            string sql = @"SELECT * FROM " + _tableName + " WHERE StateCode = 0";
            return Access.VspDbAccess.ExecuteSqlQuery(sql);
        }

        public static DataTable GetInactive()
        {
            string sql = @"SELECT * FROM " + _tableName + " WHERE StateCode = 1";
            return Access.VspDbAccess.ExecuteSqlQuery(sql);
        }

        public static DataTable GetAssociated(Advisor planAdvisor)
        {
            string sql = @"SELECT * FROM " + _tableName + " WHERE PlanAdvisorId = '" + planAdvisor.Id + "'";
            return Access.VspDbAccess.ExecuteSqlQuery(sql);
        }
    }
}
