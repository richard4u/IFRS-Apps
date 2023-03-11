﻿using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Client.Basic.Entities;
//using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
//using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Client.Basic.Contracts
{
    [ServiceContract]
    public interface IMPRPLService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region MPRGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRGLMapping UpdateMPRGLMapping(MPRGLMapping mprGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRGLMapping(int mprGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRGLMapping GetMPRGLMapping(int mprGLMappingId);

        [OperationContract]
        MPRGLMappingData[] GetAllMPRGLMappings();

        [OperationContract]
        KeyValueData[] GetUnMappedPLGLs();

        #endregion

        #region PLCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLCaption UpdatePLCaption(PLCaption plCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLCaption(int plCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLCaption GetPLCaption(int plCaptionId);

        [OperationContract]
        PLCaptionData[] GetAllPLCaptions();

        #endregion

        #region GLReclassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLReclassification UpdateGLReclassification(GLReclassification glReclassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLReclassification(int glReclassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLReclassification GetGLReclassification(int glReclassificationId);

        [OperationContract]
        GLReclassificationData[] GetAllGLReclassifications();

        #endregion

        #region GLException

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLException UpdateGLException(GLException glException);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLException(int glExceptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLException GetGLException(int glExceptionId);

        [OperationContract]
        GLExceptionData[] GetAllGLExceptions();

        #endregion

        #region GLMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLMIS UpdateGLMIS(GLMIS glMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLMIS(int glMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMIS GetGLMIS(int glMISId);

        [OperationContract]
        GLMISData[] GetAllGLMISInfo();


        #endregion

        #region MPRTotalLine

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRTotalLine UpdateMPRTotalLine(MPRTotalLine mprTotalLine);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRTotalLine(int mprTotalLineId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRTotalLine GetMPRTotalLine(int mprTotalLineId);

        [OperationContract]
        MPRTotalLineData[] GetAllMPRTotalLines();

        #endregion

        #region MPRTotalLineMakeUp

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRTotalLineMakeUp UpdateMPRTotalLineMakeUp(MPRTotalLineMakeUp mprTotalLineMakeUp);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRTotalLineMakeUp(int mprTotalLineMakeUpId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRTotalLineMakeUp GetMPRTotalLineMakeUp(int mprTotalLineMakeUpId);

        [OperationContract]
        MPRTotalLineMakeUpData[] GetAllMPRTotalLineMakeUps();

        #endregion

        #region PLIncomeReport

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLIncomeReport UpdatePLIncomeReport(PLIncomeReport plIncomeReport);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLIncomeReport(int plIncomeReportId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLIncomeReport GetPLIncomeReport(int plIncomeReportId);

        [OperationContract]
        PLIncomeReport[] GetAllPLIncomeReports();

        [OperationContract]
        PLIncomeReport[] GetPLIncomeReports(string searchType, string searchValue, int number);

        #endregion

        #region PLIncomeReportAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLIncomeReportAdjustment UpdatePLIncomeReportAdjustment(PLIncomeReportAdjustment plIncomeReportAdjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLIncomeReportAdjustment(int plIncomeReportAdjustmentd);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLIncomeReportAdjustment GetPLIncomeReportAdjustment(int plIncomeReportAdjustmentId);

        [OperationContract]
        PLIncomeReportAdjustment[] GetAllPLIncomeReportAdjustments();

        [OperationContract]
        PLIncomeReportAdjustment[] GetPLIncomeReportAdjustments(string searchType, string searchValue, int number);


        #endregion

        #region Revenue

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Revenue UpdateRevenue(Revenue revenue);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenue(int revenueId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Revenue GetRevenue(int revenueId);

        [OperationContract]
        Revenue[] GetAllRevenues();

        [OperationContract]
        Revenue[] GetRevenues(string searchType, string searchValue, int number);

        #endregion

        #region RevenueBudget

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueBudget UpdateRevenueBudget(RevenueBudget revenueBudget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueBudget(int revenueBudgetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueBudget GetRevenueBudget(int revenueBudgetId);

        [OperationContract]
        RevenueBudget[] GetAllRevenueBudgets(string year);

        #endregion


        #region RevenueBudgetOfficer

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueBudgetOfficer UpdateRevenueBudgetOfficer(RevenueBudgetOfficer revenueBudgetOfficer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueBudgetOfficer(int revenueBudgetOffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueBudgetOfficer GetRevenueBudgetOfficer(int revenueBudgetOffId);

        [OperationContract]
        RevenueBudgetOfficer[] GetAllRevenueBudgetOfficers(string year);

        #endregion

    }
}
