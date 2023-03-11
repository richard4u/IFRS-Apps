using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Xml;

namespace Fintrak.Shared.AuditService
{
    public class AuditManager
    {
        private DataContext _context;

        public AuditManager()
        {

        }


        public AuditManager(string connection)
        {
            _context = new DataContext(connection);
        }

        public void AddAudit(DbEntityEntry entry,string loginName)
        {
            _context.AuditTrailFactory(entry,loginName);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public AuditTrail Get(int id)
        {
            var repo = new AuditTrailRepository();
            return repo.Get(id);
        }

        public IEnumerable<AuditTrail> Get()
        {
            var repo = new AuditTrailRepository();
            return repo.Get();
        }

        public IEnumerable<AuditTrail> GetByDate(DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByDate(fromDate, toDate);
        }

        public IEnumerable<AuditTrail> GetAuditTrailByTab(AuditAction action)
        {
            var repo = new AuditTrailRepository();
            return repo.GetAuditTrailByTab(action);
        }

        public IEnumerable<AuditTrail> GetByTable(string tablename, DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByTable(tablename,fromDate, toDate);
        }

        public IEnumerable<AuditTrail> GetByLoginID(string loginID, DateTime fromDate, DateTime toDate)
        {
            var repo = new AuditTrailRepository();
            return repo.GetByLoginID(loginID,fromDate, toDate);
        }

        //public List<AuditTrail> GetByAction(string action, DateTime fromDate, DateTime toDate)
        //{
        //    var repo = new AuditTrailRepository();
        //    return repo.GetByAction(action,fromDate, toDate);
        //}

        public List<AuditTrail> GetByAction(string action, DateTime fromDate, DateTime toDate)
        {
            //var connectionString = DataContext.GetDataConnection();
            AuditAction action2 = (AuditAction)Enum.Parse(typeof(AuditAction), action); //convert string to enum

            var query = new List<AuditTrail>();
            var audTrails = new List<AuditTrail>();

            List<string> cArray = new List<string> { "active", "deleted", "createdby", "createdon", "updatedby", "updatedon", "rowversion" };
            cArray = cArray.ConvertAll(d => d.ToLower()); //convert all teh string the array to lowercase

            using (DataContext entityContext = new DataContext())
            {
                query = (from a in entityContext.AuditTrailSet
                         where a.Actions == action2 && a.RevisionStamp >= fromDate && a.RevisionStamp <= toDate
                         select a).ToList();
            }

            foreach (var v in query)
            {
                var audTrail = new AuditTrail();

                audTrail.AuditTrailId = v.AuditTrailId;
                int enumaction = (int)v.Actions;

                DateTime outDate = DateTime.Now;
                DateTime.TryParse(v.RevisionStamp.ToString(), out outDate);
                audTrail.RevisionStamp = outDate;

                audTrail.TableName = v.TableName;
                audTrail.UserName = v.UserName;

                //if (Convert.ToInt32(v.Actions)==1)
                //    audTrail.Actions = AuditAction.C;
                //else if (Convert.ToInt32(v.Actions) == 2)
                //    audTrail.Actions = AuditAction.U;
                //else if (Convert.ToInt32(v.Actions) == 3)
                //    audTrail.Actions = AuditAction.D;
                //else if (Convert.ToInt32(v.Actions) == 4)
                //    audTrail.Actions = AuditAction.E;                

                if (enumaction == 1)
                {
                    audTrail.Actions = AuditAction.C;

                    XmlDocument xmlContent_new = new XmlDocument();
                    v.NewData = v.NewData.Replace("\r\n", " ");
                    xmlContent_new.LoadXml(v.NewData);
                    XmlElement elm = xmlContent_new.DocumentElement;
                    XmlNodeList childNodesList = elm.ChildNodes;
                    int cnsize = childNodesList.Count;
                    int c = 0;
                    string actiondescription = "Create record ";

                    while (c <= cnsize - 1)
                    {
                        if (cArray.IndexOf(childNodesList[c].Name.ToString().ToLower()) < 0)
                        {
                            string nodename = childNodesList[c].Name.ToString();
                            string nodevalue = childNodesList[c].InnerText;

                            actiondescription = actiondescription + nodename + " = " + nodevalue + ", ";
                        }
                        c = c + 1;
                    }
                    audTrail.ActionDescription = actiondescription;
                }
                else if (enumaction == 2)
                {
                    audTrail.Actions = AuditAction.U;

                    if (!string.IsNullOrEmpty(v.ChangedColumns) && !string.IsNullOrEmpty(v.OldData) && !string.IsNullOrEmpty(v.NewData))
                    {
                        XmlDocument xmlContent_old = new XmlDocument();
                        v.OldData = v.OldData.Replace("\r\n", " ") ?? "";
                        xmlContent_old.LoadXml(v.OldData);
                        XmlElement elm_old = xmlContent_old.DocumentElement;
                        //XmlNodeList old_childNodesList = elm_old.ChildNodes;

                        XmlDocument xmlContent_new = new XmlDocument();
                        v.NewData = v.NewData.Replace("\r\n", " ") ?? "";
                        xmlContent_new.LoadXml(v.NewData);
                        XmlElement elm_new = xmlContent_new.DocumentElement;
                        //XmlNodeList new_childNodesList = elm_new.ChildNodes;

                        XmlDocument xmlContent_changedcolumn = new XmlDocument();
                        v.ChangedColumns = v.ChangedColumns.Replace("\r\n", " ") ?? "";
                        xmlContent_changedcolumn.LoadXml(v.ChangedColumns);
                        XmlElement elm_changedcolumn = xmlContent_changedcolumn.DocumentElement;
                        XmlNodeList changedcolumn_childNodesList = elm_changedcolumn.ChildNodes;

                        int cnsize = changedcolumn_childNodesList.Count;
                        int c = 0;
                        string actiondescription = "Modify Column(s) ";

                        while (c <= cnsize - 1)
                        {
                            if (changedcolumn_childNodesList.Count > 0)
                            {
                                if (cArray.IndexOf(changedcolumn_childNodesList[c].InnerText.ToLower()) < 0)
                                {
                                    string changedcolumn = changedcolumn_childNodesList[c].InnerText;

                                    var oldmatchingNode = elm_old.GetElementsByTagName(changedcolumn);
                                    string oldnodevalue = "";
                                    if (oldmatchingNode.Count > 0)
                                        oldnodevalue = oldmatchingNode[0].InnerText;

                                    var newmatchingNode = elm_new.GetElementsByTagName(changedcolumn);
                                    string newnodevalue = "";
                                    if (newmatchingNode.Count > 0)
                                        newnodevalue = newmatchingNode[0].InnerText;

                                    if (oldnodevalue != newnodevalue)
                                        actiondescription = actiondescription + changedcolumn + " from " + oldnodevalue + " to " + newnodevalue + ", ";

                                }
                            }
                            c = c + 1;
                        }
                        audTrail.ActionDescription = actiondescription;
                    }
                }
                else if (enumaction == 3)
                {
                    audTrail.Actions = AuditAction.D;

                    XmlDocument xmlContent_old = new XmlDocument();
                    v.OldData = v.OldData.Replace("\r\n", " ");
                    xmlContent_old.LoadXml(v.OldData);
                    XmlElement elm_old = xmlContent_old.DocumentElement;
                    XmlNodeList old_childNodesList = elm_old.ChildNodes;
                    int cnsize = old_childNodesList.Count;
                    int c = 0;
                    string actiondescription = "Delete record with Column(s) ";

                    while (c <= cnsize - 1)
                    {
                        if (cArray.IndexOf(old_childNodesList[c].Name.ToString().ToLower()) < 0)
                        {
                            string nodename = old_childNodesList[c].Name.ToString();
                            string nodevalue = old_childNodesList[c].InnerText;

                            actiondescription = actiondescription + nodename + " = " + nodevalue + ", ";
                        }
                        c = c + 1;
                    }
                    audTrail.ActionDescription = actiondescription;
                }

                audTrails.Add(audTrail);
            }


            return audTrails;
        }
    }
}
