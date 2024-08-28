using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;

using TWS_Business.Depots;
using TWS_Business.Sets;
using TWS_Customer.Services.Interfaces;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Customer.Services;
public class YardLogsService : IYardLogsService {
    private readonly YardLogsDepot YardLogs;

    public YardLogsService(
       YardLogsDepot YardLogs) {
        
        this.YardLogs = YardLogs;
   
    }

    public async Task<SetViewOut<YardLog>> View(SetViewOptions options) {

        static IQueryable<YardLog> include(IQueryable<YardLog> query) {
            return query
            .Include(t => t.DriverExternalNavigation)
                .ThenInclude(i => i!.IdentificationNavigation)
            .Include(t => t.DriverExternalNavigation)
                .ThenInclude(i => i!.DriverCommonNavigation)

            .Include(t => t.DriverNavigation)
                .ThenInclude(d => d!.EmployeeNavigation)
                    .ThenInclude(e => e!.IdentificationNavigation)

            .Include(t => t.DriverNavigation)
                .ThenInclude(d => d!.DriverCommonNavigation)

            .Include(t => t.TrailerExternalNavigation)
                .ThenInclude(d => d!.TrailerCommonNavigation)

            .Include(t => t.TrailerNavigation)
                .ThenInclude(d => d!.TrailerCommonNavigation)

            .Include(t => t.TruckNavigation)
                .ThenInclude(t => t!.TruckCommonNavigation)

            .Include(t => t.TruckExternalNavigation)
                .ThenInclude(t => t!.TruckCommonNavigation)
            .Include(t => t.TruckExternalNavigation)
                .ThenInclude(t => t!.TruckCommonNavigation)

            .Include(t => t.LoadTypeNavigation)

            .Include(t => t.SectionNavigation)
                .ThenInclude(t => t!.LocationNavigation)
            .Select(y => new YardLog() {
                Id = y.Id,
                Entry = y.Entry,
                Timestamp = y.Timestamp,
                Truck = y.Truck,
                TruckExternal = y.TruckExternal,
                Trailer = y.Trailer,
                TrailerExternal = y.TrailerExternal,
                LoadType = y.LoadType,
                Guard = y.Guard,
                Gname = y.Gname,
                Section = y.Section,
                FromTo = y.FromTo,
                Damage = y.Damage,
                TTPicture = y.TTPicture,
                DmgEvidence = y.DmgEvidence,
                Driver = y.Driver,
                DriverExternal = y.DriverExternal,
                DriverExternalNavigation = y.DriverExternalNavigation == null ? null : new DriverExternal() {
                    Id = y.DriverExternalNavigation.Id,
                    Status = y.DriverExternalNavigation.Status,
                    Common = y.DriverExternalNavigation.Common,
                    Identification = y.DriverExternalNavigation.Identification,
                    DriverCommonNavigation = y.DriverExternalNavigation.DriverCommonNavigation == null ? null : new DriverCommon() {
                        Id = y.DriverExternalNavigation.DriverCommonNavigation.Id,
                        License = y.DriverExternalNavigation.DriverCommonNavigation.License,
                        Situation = y.DriverExternalNavigation.DriverCommonNavigation.Situation,
                    },
                    IdentificationNavigation = y.DriverExternalNavigation.IdentificationNavigation == null ? null : new Identification() {
                        Id = y.DriverExternalNavigation.IdentificationNavigation.Id,
                        Status = y.DriverExternalNavigation.IdentificationNavigation.Status,
                        Name = y.DriverExternalNavigation.IdentificationNavigation.Name,
                        FatherLastname = y.DriverExternalNavigation.IdentificationNavigation.FatherLastname,
                        MotherLastName = y.DriverExternalNavigation.IdentificationNavigation.MotherLastName,
                        Birthday = y.DriverExternalNavigation.IdentificationNavigation.Birthday
                    }
                },
                DriverNavigation = y.DriverNavigation == null ? null : new Driver() {
                    Id = y.DriverNavigation.Id,
                    Status = y.DriverNavigation.Status,
                    Employee = y.DriverNavigation.Employee,
                    Common = y.DriverNavigation.Common,
                    DriverType = y.DriverNavigation.DriverType,
                    LicenseExpiration = y.DriverNavigation.LicenseExpiration,
                    DrugalcRegistrationDate = y.DriverNavigation.DrugalcRegistrationDate,
                    PullnoticeRegistrationDate = y.DriverNavigation.PullnoticeRegistrationDate,
                    Twic = y.DriverNavigation.Twic,
                    TwicExpiration = y.DriverNavigation.TwicExpiration,
                    Visa = y.DriverNavigation.Visa,
                    VisaExpiration = y.DriverNavigation.VisaExpiration,
                    Fast = y.DriverNavigation.Fast,
                    FastExpiration = y.DriverNavigation.FastExpiration,
                    Anam = y.DriverNavigation.Anam,
                    AnamExpiration = y.DriverNavigation.AnamExpiration,
                    EmployeeNavigation = y.DriverNavigation.EmployeeNavigation == null ? null : new Employee() {
                        Id = y.DriverNavigation.EmployeeNavigation.Id,
                        Status = y.DriverNavigation.EmployeeNavigation.Status,
                        Identification = y.DriverNavigation.EmployeeNavigation.Identification,
                        Address = y.DriverNavigation.EmployeeNavigation.Address,
                        Approach = y.DriverNavigation.EmployeeNavigation.Approach,
                        Curp = y.DriverNavigation.EmployeeNavigation.Curp,
                        AntecedentesNoPenaleseExp = y.DriverNavigation.EmployeeNavigation.AntecedentesNoPenaleseExp,
                        Rfc = y.DriverNavigation.EmployeeNavigation.Rfc,
                        Nss = y.DriverNavigation.EmployeeNavigation.Nss,
                        IMSSRegistrationDate = y.DriverNavigation.EmployeeNavigation.IMSSRegistrationDate,
                        HiringDate = y.DriverNavigation.EmployeeNavigation.HiringDate,
                        TerminationDate = y.DriverNavigation.EmployeeNavigation.TerminationDate,
                        IdentificationNavigation = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation == null ? null : new Identification() {
                            Id = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Id,
                            Status = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Status,
                            Name = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Name,
                            FatherLastname = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.FatherLastname,
                            MotherLastName = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.MotherLastName,
                            Birthday = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Birthday
                        }
                    },
                    DriverCommonNavigation = y.DriverNavigation.DriverCommonNavigation == null ? null : new DriverCommon() {
                        Id = y.DriverNavigation.DriverCommonNavigation.Id,
                        License = y.DriverNavigation.DriverCommonNavigation.License,
                        Situation = y.DriverNavigation.DriverCommonNavigation.Situation,
                    },
                },
                TrailerExternalNavigation = y.TrailerExternalNavigation == null ? null : new TrailerExternal() {
                    Id = y.TrailerExternalNavigation.Id,
                    Status = y.TrailerExternalNavigation.Status,
                    Common = y.TrailerExternalNavigation.Common,
                    TrailerCommonNavigation = y.TrailerExternalNavigation.TrailerCommonNavigation == null ? null : new TrailerCommon() {
                        Id = y.TrailerExternalNavigation.TrailerCommonNavigation.Id,
                        Economic = y.TrailerExternalNavigation.TrailerCommonNavigation.Economic,
                        Class = y.TrailerExternalNavigation.TrailerCommonNavigation.Class,
                        Carrier = y.TrailerExternalNavigation.TrailerCommonNavigation.Carrier,
                        Situation = y.TrailerExternalNavigation.TrailerCommonNavigation.Situation,
                        Location = y.TrailerExternalNavigation.TrailerCommonNavigation.Location,
                        Plates = (ICollection<Plate>)y.TrailerExternalNavigation.TrailerCommonNavigation.Plates.Select(p => new Plate() {
                            Id = p.Id,
                            Status = p.Status,
                            Identifier = p.Identifier,
                            State = p.State,
                            Country = p.Country,
                            Expiration = p.Expiration,
                            Truck = p.Truck,
                            Trailer = p.Trailer
                        })
                    }
                },
                TrailerNavigation = y.TrailerNavigation == null ? null : new Trailer() {
                    Id = y.TrailerNavigation.Id,
                    Status = y.TrailerNavigation.Status,
                    Common = y.TrailerNavigation.Common,
                    Manufacturer = y.TrailerNavigation.Manufacturer,
                    Maintenance = y.TrailerNavigation.Maintenance,
                    TrailerCommonNavigation = y.TrailerNavigation.TrailerCommonNavigation == null ? null : new TrailerCommon() {
                        Id = y.TrailerNavigation.TrailerCommonNavigation.Id,
                        Economic = y.TrailerNavigation.TrailerCommonNavigation.Economic,
                        Class = y.TrailerNavigation.TrailerCommonNavigation.Class,
                        Carrier = y.TrailerNavigation.TrailerCommonNavigation.Carrier,
                        Situation = y.TrailerNavigation.TrailerCommonNavigation.Situation,
                        Location = y.TrailerNavigation.TrailerCommonNavigation.Location,
                        Plates = (ICollection<Plate>)y.TrailerNavigation.TrailerCommonNavigation.Plates.Select(p => new Plate() {
                            Id = p.Id,
                            Status = p.Status,
                            Identifier = p.Identifier,
                            State = p.State,
                            Country = p.Country,
                            Expiration = p.Expiration,
                            Truck = p.Truck,
                            Trailer = p.Trailer
                        })
                    }
                },
                TruckExternalNavigation = y.TruckExternalNavigation == null ? null : new TruckExternal() {
                    Id = y.TruckExternalNavigation.Id,
                    Status = y.TruckExternalNavigation.Status,
                    Common = y.TruckExternalNavigation.Common,
                    TruckCommonNavigation = y.TruckExternalNavigation.TruckCommonNavigation == null ? null : new TruckCommon() {
                        Id = y.TruckExternalNavigation.TruckCommonNavigation.Id,
                        Vin = y.TruckExternalNavigation.TruckCommonNavigation.Vin,
                        Economic = y.TruckExternalNavigation.TruckCommonNavigation.Economic,
                        Carrier = y.TruckExternalNavigation.TruckCommonNavigation.Carrier,
                        Location = y.TruckExternalNavigation.TruckCommonNavigation.Location,
                        Situation = y.TruckExternalNavigation.TruckCommonNavigation.Situation,
                        Plates = (ICollection<Plate>)y.TruckExternalNavigation.TruckCommonNavigation.Plates.Select(p => new Plate() {
                            Id = p.Id,
                            Status = p.Status,
                            Identifier = p.Identifier,
                            State = p.State,
                            Country = p.Country,
                            Expiration = p.Expiration,
                            Truck = p.Truck,
                            Trailer = p.Trailer
                        })
                    }
                },
                TruckNavigation = y.TruckNavigation == null ? null : new Truck() {
                    Id = y.TruckNavigation.Id,
                    Status = y.TruckNavigation.Status,
                    Common = y.TruckNavigation.Common,
                    Motor = y.TruckNavigation.Motor,
                    Manufacturer = y.TruckNavigation.Manufacturer,
                    Maintenance = y.TruckNavigation.Maintenance,
                    Insurance = y.TruckNavigation.Insurance,
                    TruckCommonNavigation = y.TruckNavigation.TruckCommonNavigation == null ? null : new TruckCommon() {
                        Id = y.TruckNavigation.TruckCommonNavigation.Id,
                        Vin = y.TruckNavigation.TruckCommonNavigation.Vin,
                        Economic = y.TruckNavigation.TruckCommonNavigation.Economic,
                        Carrier = y.TruckNavigation.TruckCommonNavigation.Carrier,
                        Location = y.TruckNavigation.TruckCommonNavigation.Location,
                        Situation = y.TruckNavigation.TruckCommonNavigation.Situation,
                        Plates = (ICollection<Plate>)y.TruckNavigation.TruckCommonNavigation.Plates.Select(p => new Plate() {
                            Id = p.Id,
                            Status = p.Status,
                            Identifier = p.Identifier,
                            State = p.State,
                            Country = p.Country,
                            Expiration = p.Expiration,
                            Truck = p.Truck,
                            Trailer = p.Trailer
                        })
                    }
                },
                LoadTypeNavigation = y.LoadTypeNavigation == null ? null : new LoadType() {
                    Id = y.LoadTypeNavigation.Id,
                    Name = y.LoadTypeNavigation.Name,
                    Description = y.LoadTypeNavigation.Description
                },
                SectionNavigation = y.SectionNavigation == null ? null : new Section() {
                    Id = y.SectionNavigation.Id,
                    Status = y.SectionNavigation.Status,
                    Name = y.SectionNavigation.Name,
                    Yard = y.SectionNavigation.Yard,
                    Capacity = y.SectionNavigation.Capacity,
                    Ocupancy = y.SectionNavigation.Ocupancy,
                    LocationNavigation = y.SectionNavigation.LocationNavigation == null ? null : new Location() {
                        Id = y.SectionNavigation.LocationNavigation.Id,
                        Status = y.SectionNavigation.LocationNavigation.Status,
                        Name = y.SectionNavigation.LocationNavigation.Name,
                        Address = y.SectionNavigation.LocationNavigation.Address
                    }
                }


            });

        }
        return await YardLogs.View(options, include);
    }

    public async Task<SourceTransactionOut<YardLog>> Create(YardLog[] yardLog) {
        return await this.YardLogs.Create(yardLog);
    }
    public async Task<RecordUpdateOut<YardLog>> Update(YardLog yardLog, bool updatePivot = false) {
        
        static IQueryable<YardLog> include(IQueryable<YardLog> query) {
            return query
            .Include(t => t.DriverExternalNavigation)
                .ThenInclude(i => i!.IdentificationNavigation)
            .Include(t => t.DriverExternalNavigation)
                .ThenInclude(i => i!.DriverCommonNavigation)

            .Include(t => t.DriverNavigation)
                .ThenInclude(d => d!.EmployeeNavigation)
                    .ThenInclude(e => e!.IdentificationNavigation)

            .Include(t => t.DriverNavigation)
                .ThenInclude(d => d!.DriverCommonNavigation)

            .Include(t => t.TrailerExternalNavigation)
                .ThenInclude(d => d!.TrailerCommonNavigation)

            .Include(t => t.TrailerNavigation)
                .ThenInclude(d => d!.TrailerCommonNavigation)

            .Include(t => t.TruckNavigation)
                .ThenInclude(t => t!.TruckCommonNavigation)

            .Include(t => t.TruckExternalNavigation)
                .ThenInclude(t => t!.TruckCommonNavigation)

            .Include(t => t.LoadTypeNavigation)

            .Include(t => t.SectionNavigation)
                .ThenInclude(t => t!.LocationNavigation)
            .Select(y => new YardLog() {
                Id = y.Id,
                Entry = y.Entry,
                Timestamp = y.Timestamp,
                Truck = y.Truck,
                TruckExternal = y.TruckExternal,
                Trailer = y.Trailer,
                TrailerExternal = y.TrailerExternal,
                LoadType = y.LoadType,
                Guard = y.Guard,
                Gname = y.Gname,
                Section = y.Section,
                FromTo = y.FromTo,
                Damage = y.Damage,
                TTPicture = y.TTPicture,
                DmgEvidence = y.DmgEvidence,
                Driver = y.Driver,
                DriverExternal = y.DriverExternal,
                DriverExternalNavigation = y.DriverExternalNavigation == null ? null : new DriverExternal() { 
                    Id = y.DriverExternalNavigation.Id,
                    Status = y.DriverExternalNavigation.Status,
                    Common = y.DriverExternalNavigation.Common,
                    Identification = y.DriverExternalNavigation.Identification,
                    DriverCommonNavigation = y.DriverExternalNavigation.DriverCommonNavigation == null ? null : new DriverCommon() {
                        Id = y.DriverExternalNavigation.DriverCommonNavigation.Id,
                        License = y.DriverExternalNavigation.DriverCommonNavigation.License,
                        Situation = y.DriverExternalNavigation.DriverCommonNavigation.Situation,
                    },
                    IdentificationNavigation = y.DriverExternalNavigation.IdentificationNavigation == null ? null : new Identification() { 
                        Id = y.DriverExternalNavigation.IdentificationNavigation.Id,
                        Status = y.DriverExternalNavigation.IdentificationNavigation.Status,
                        Name = y.DriverExternalNavigation.IdentificationNavigation.Name,
                        FatherLastname = y.DriverExternalNavigation.IdentificationNavigation.FatherLastname,
                        MotherLastName = y.DriverExternalNavigation.IdentificationNavigation.MotherLastName,
                        Birthday = y.DriverExternalNavigation.IdentificationNavigation.Birthday
                    }
                },
                DriverNavigation = y.DriverNavigation == null ? null : new Driver() {
                    Id = y.DriverNavigation.Id,
                    Status = y.DriverNavigation.Status,
                    Employee = y.DriverNavigation.Employee,
                    Common = y.DriverNavigation.Common,
                    DriverType = y.DriverNavigation.DriverType,
                    LicenseExpiration = y.DriverNavigation.LicenseExpiration,
                    DrugalcRegistrationDate = y.DriverNavigation.DrugalcRegistrationDate,
                    PullnoticeRegistrationDate = y.DriverNavigation.PullnoticeRegistrationDate,
                    Twic = y.DriverNavigation.Twic,
                    TwicExpiration = y.DriverNavigation.TwicExpiration,
                    Visa = y.DriverNavigation.Visa,
                    VisaExpiration = y.DriverNavigation.VisaExpiration,
                    Fast = y.DriverNavigation.Fast,
                    FastExpiration = y.DriverNavigation.FastExpiration,
                    Anam = y.DriverNavigation.Anam,
                    AnamExpiration = y.DriverNavigation.AnamExpiration,
                    EmployeeNavigation = y.DriverNavigation.EmployeeNavigation == null ? null : new Employee() {
                        Id = y.DriverNavigation.EmployeeNavigation.Id,
                        Status = y.DriverNavigation.EmployeeNavigation.Status,
                        Identification = y.DriverNavigation.EmployeeNavigation.Identification,
                        Address = y.DriverNavigation.EmployeeNavigation.Address,
                        Approach = y.DriverNavigation.EmployeeNavigation.Approach,
                        Curp = y.DriverNavigation.EmployeeNavigation.Curp,
                        AntecedentesNoPenaleseExp = y.DriverNavigation.EmployeeNavigation.AntecedentesNoPenaleseExp,
                        Rfc = y.DriverNavigation.EmployeeNavigation.Rfc,
                        Nss = y.DriverNavigation.EmployeeNavigation.Nss,
                        IMSSRegistrationDate = y.DriverNavigation.EmployeeNavigation.IMSSRegistrationDate,
                        HiringDate = y.DriverNavigation.EmployeeNavigation.HiringDate,
                        TerminationDate = y.DriverNavigation.EmployeeNavigation.TerminationDate,
                        IdentificationNavigation = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation == null ? null : new Identification() {
                            Id = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Id,
                            Status = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Status,
                            Name = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Name,
                            FatherLastname = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.FatherLastname,
                            MotherLastName = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.MotherLastName,
                            Birthday = y.DriverNavigation.EmployeeNavigation.IdentificationNavigation.Birthday
                        }
                    },
                    DriverCommonNavigation = y.DriverNavigation.DriverCommonNavigation == null ? null : new DriverCommon() {
                        Id = y.DriverNavigation.DriverCommonNavigation.Id,
                        License = y.DriverNavigation.DriverCommonNavigation.License,
                        Situation = y.DriverNavigation.DriverCommonNavigation.Situation,
                    },
                },
                TrailerExternalNavigation = y.TrailerExternalNavigation == null ? null : new TrailerExternal() { 
                    Id = y.TrailerExternalNavigation.Id,
                    Status = y.TrailerExternalNavigation.Status,
                    Common = y.TrailerExternalNavigation.Common,
                    TrailerCommonNavigation = y.TrailerExternalNavigation.TrailerCommonNavigation == null ? null : new TrailerCommon() { 
                        Id = y.TrailerExternalNavigation.TrailerCommonNavigation.Id,
                        Economic = y.TrailerExternalNavigation.TrailerCommonNavigation.Economic,
                        Class = y.TrailerExternalNavigation.TrailerCommonNavigation.Class,
                        Carrier = y.TrailerExternalNavigation.TrailerCommonNavigation.Carrier,
                        Situation = y.TrailerExternalNavigation.TrailerCommonNavigation.Situation,
                        Location = y.TrailerExternalNavigation.TrailerCommonNavigation.Location
                    }
                },
                TrailerNavigation = y.TrailerNavigation == null ? null : new Trailer() { 
                    Id = y.TrailerNavigation.Id,
                    Status = y.TrailerNavigation.Status,
                    Common = y.TrailerNavigation.Common,
                    Manufacturer = y.TrailerNavigation.Manufacturer,
                    Maintenance = y.TrailerNavigation.Maintenance,
                    TrailerCommonNavigation = y.TrailerNavigation.TrailerCommonNavigation == null ? null : new TrailerCommon() {
                        Id = y.TrailerNavigation.TrailerCommonNavigation.Id,
                        Economic = y.TrailerNavigation.TrailerCommonNavigation.Economic,
                        Class = y.TrailerNavigation.TrailerCommonNavigation.Class,
                        Carrier = y.TrailerNavigation.TrailerCommonNavigation.Carrier,
                        Situation = y.TrailerNavigation.TrailerCommonNavigation.Situation,
                        Location = y.TrailerNavigation.TrailerCommonNavigation.Location
                    }
                },
                TruckExternalNavigation = y.TruckExternalNavigation == null ? null : new TruckExternal() {
                    Id = y.TruckExternalNavigation.Id,
                    Status = y.TruckExternalNavigation.Status,
                    Common = y.TruckExternalNavigation.Common,
                    TruckCommonNavigation = y.TruckExternalNavigation.TruckCommonNavigation == null ? null : new TruckCommon() {
                        Id = y.TruckExternalNavigation.TruckCommonNavigation.Id,
                        Vin = y.TruckExternalNavigation.TruckCommonNavigation.Vin,
                        Economic = y.TruckExternalNavigation.TruckCommonNavigation.Economic,
                        Carrier = y.TruckExternalNavigation.TruckCommonNavigation.Carrier,
                        Location = y.TruckExternalNavigation.TruckCommonNavigation.Location,
                        Situation = y.TruckExternalNavigation.TruckCommonNavigation.Situation
                    }
                },
                TruckNavigation = y.TruckNavigation == null ? null : new Truck() {
                    Id = y.TruckNavigation.Id,
                    Status = y.TruckNavigation.Status,
                    Common = y.TruckNavigation.Common,
                    Motor = y.TruckNavigation.Motor,
                    Manufacturer = y.TruckNavigation.Manufacturer,
                    Maintenance = y.TruckNavigation.Maintenance,
                    Insurance = y.TruckNavigation.Insurance,
                    TruckCommonNavigation = y.TruckNavigation.TruckCommonNavigation == null ? null : new TruckCommon() {
                        Id = y.TruckNavigation.TruckCommonNavigation.Id,
                        Vin = y.TruckNavigation.TruckCommonNavigation.Vin,
                        Economic = y.TruckNavigation.TruckCommonNavigation.Economic,
                        Carrier = y.TruckNavigation.TruckCommonNavigation.Carrier,
                        Location = y.TruckNavigation.TruckCommonNavigation.Location,
                        Situation = y.TruckNavigation.TruckCommonNavigation.Situation
                    }
                },
                LoadTypeNavigation = y.LoadTypeNavigation == null ? null : new LoadType() {
                    Id = y.LoadTypeNavigation.Id,
                    Name = y.LoadTypeNavigation.Name,
                    Description = y.LoadTypeNavigation.Description
                },
                SectionNavigation = y.SectionNavigation == null ? null : new Section() {
                    Id = y.SectionNavigation.Id,
                    Status = y.SectionNavigation.Status,
                    Name = y.SectionNavigation.Name,
                    Yard = y.SectionNavigation.Yard,
                    Capacity = y.SectionNavigation.Capacity,
                    Ocupancy = y.SectionNavigation.Ocupancy,
                    LocationNavigation = y.SectionNavigation.LocationNavigation == null ? null : new Location() {
                        Id = y.SectionNavigation.LocationNavigation.Id,
                        Status = y.SectionNavigation.LocationNavigation.Status,
                        Name = y.SectionNavigation.LocationNavigation.Name,
                        Address = y.SectionNavigation.LocationNavigation.Address
                    }
                }
                

            });
            
        }

        return await YardLogs.Update(yardLog, include) ;
    }

    public async Task<YardLog> Delete(int Id) {
        return await YardLogs.Delete(Id);
    }
}
