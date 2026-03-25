using CsvHelper.Configuration;
using Energy.Domain.Models;

namespace UKModel.DbLoader.Repd;

public sealed class RepdCsvMap : ClassMap<RenewableEnergyProject>
{
    public RepdCsvMap()
    {
        Map(m => m.Id).Ignore();
        Map(m => m.Latitude).Ignore();
        Map(m => m.Longitude).Ignore();
        Map(m => m.OldRefId).Name("Old Ref ID");
        Map(m => m.RefId).Name("Ref ID");
        Map(m => m.RecordLastUpdated).Name("Record Last Updated (dd/mm/yyyy)").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.Operator).Name("Operator (or Applicant)");
        Map(m => m.SiteName).Name("Site Name");
        Map(m => m.TechnologyType).Name("Technology Type").TypeConverter<TechnologyTypeConverter>();
        Map(m => m.StorageType).Name("Storage Type");
        Map(m => m.StorageCoLocationRepdRefId).Name("Storage Co-location REPD Ref ID");
        Map(m => m.InstalledCapacityMWe).Name("Installed Capacity (MWelec)");
        Map(m => m.ShareCommunityScheme).Name("Share Community Scheme");
        Map(m => m.ChpEnabled).Name("CHP Enabled").TypeConverter<YesNoBoolConverter>();
        Map(m => m.CfdAllocationRound).Name("CfD Allocation Round");
        Map(m => m.RoBandingRocPerMWh).Name("RO Banding (ROC/MWh)");
        Map(m => m.FitTariffPencePerKWh).Name("FiT Tariff (p/kWh)");
        Map(m => m.CfdCapacityMW).Name("CfD Capacity (MW)");
        Map(m => m.TurbineCapacityMW).Name("Turbine Capacity (MW)");
        Map(m => m.NumberOfTurbines).Name("No. of Turbines");
        Map(m => m.HeightOfTurbinesMetres).Name("Height of Turbines (m)");
        Map(m => m.MountingTypeForSolar).Name("Mounting Type for Solar");
        Map(m => m.DevelopmentStatus).Name("Development Status");
        Map(m => m.DevelopmentStatusShort).Name("Development Status (short)");
        Map(m => m.ReApplyingNewRepdRef).Name("Are they re-applying (New REPD Ref)");
        Map(m => m.ReApplyingOldRepdRef).Name("Are they re-applying (Old REPD Ref)");
        Map(m => m.Address).Name("Address");
        Map(m => m.County).Name("County");
        Map(m => m.Region).Name("Region");
        Map(m => m.Country).Name("Country");
        Map(m => m.PostCode).Name("Post Code");
        Map(m => m.XCoordinate).Name("X-coordinate");
        Map(m => m.YCoordinate).Name("Y-coordinate");
        Map(m => m.PlanningAuthority).Name("Planning Authority");
        Map(m => m.PlanningApplicationReference).Name("Planning Application Reference");
        Map(m => m.AppealReference).Name("Appeal Reference");
        Map(m => m.SecretaryOfStateReference).Name("Secretary of State Reference");
        Map(m => m.TypeOfSecretaryOfStateIntervention).Name("Type of Secretary of State Intervention");
        Map(m => m.JudicialReview).Name("Judicial Review");
        Map(m => m.OffshoreWindRound).Name("Offshore Wind Round");
        Map(m => m.PlanningApplicationSubmitted).Name("Planning Application Submitted").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.PlanningApplicationWithdrawn).Name("Planning Application Withdrawn").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.PlanningPermissionRefused).Name("Planning Permission Refused").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.AppealLodged).Name("Appeal Lodged").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.AppealWithdrawn).Name("Appeal Withdrawn").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.AppealRefused).Name("Appeal Refused").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.AppealGranted).Name("Appeal Granted").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.PlanningPermissionGranted).Name("Planning Permission  Granted").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.SecretaryOfStateIntervened).Name("Secretary of State - Intervened").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.SecretaryOfStateRefusal).Name("Secretary of State - Refusal").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.SecretaryOfStateGranted).Name("Secretary of State - Granted").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.PlanningPermissionExpired).Name("Planning Permission Expired").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.UnderConstruction).Name("Under Construction").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.Operational).Name("Operational").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.HeatNetworkRef).Name("Heat Network Ref");
        Map(m => m.SolarSiteAreaSqm).Name("Solar Site Area (sqm)");
    }
}
