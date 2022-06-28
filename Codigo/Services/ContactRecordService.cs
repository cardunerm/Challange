using DesafioLaNacion.DataModel;
using DesafioLaNacion.Dtos;
using DesafioLaNacion.Infraestructura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;




namespace DesafioLaNacion.Services
{
    public class ContactRecordService
    {
        internal readonly SQLContext _contextMySql;
        public ContactRecordService(SQLContext contextMySql)
        {
            _contextMySql = contextMySql;
        }
        
        public async Task<OperationResponse<DtoGetContactRecord>> GetById(long id)
        {
            ContactRecord contact = await _contextMySql
                        .ContactRecords
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false)
                        .ConfigureAwait(false);

            // Comprobación: si el FAQ existe
            if (contact == null)

                return new OperationResponse<DtoGetContactRecord>(null, false, "El Contacto ingresado no existe");


            var dto = new DtoGetContactRecord()
            {
                Ciudad = contact.Ciudad,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Adress = contact.Adress,
                WorkPhoneNumber = contact.WorkPhoneNumber,
                PersonalPhoneNumber = contact.PersonalPhoneNumber,
                ProfileImage = contact.ProfileImage,
                Company = contact.Company,
                Email = contact.Email
            };
            return new OperationResponse<DtoGetContactRecord>(dto);
        }
        public async Task<OperationResponse<long>> Post(DtoAddContactRecord createrequest)
        {
            #region Comprobaciones
            //Compruebo si existe alguna persona con ese mismo email
            if (await _contextMySql.ContactRecords.AnyAsync(c => c.Email == createrequest.Email))
            {
                return new OperationResponse<long>(0, false, "Ya existe una persona con el mismo e-mail");
            }
            // Comprobaciones de Nombre y Apellido (length)
            if (createrequest.FirstName.Length < 2)
            {
                return new OperationResponse<long>(0, false, "El Nombre debe tener como mínimo 2 caracteres");
            }

            if (createrequest.FirstName.Length > 19)
            {
                return new OperationResponse<long>(0, false, "El Nombre debe tener como máximo 20 caracteres");
            }

            if (createrequest.LastName.Length < 2)
            {
                return new OperationResponse<long>(0, false, "El Apellido debe tener como mínimo 2 caracteres");
            }

            if (createrequest.LastName.Length > 19)
            {
                return new OperationResponse<long>(0, false, "El Apellido debe tener como máximo 20 caracteres");
            }

            // Comprobación de formato de E-Mail
            static bool IsValid(string emailaddress)
            {
                try
                {
                    MailAddress m = new(emailaddress);

                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
            if (!IsValid(createrequest.Email))
            {
                return new OperationResponse<long>(0, false, "Ingrese un E-mail válido.");
            }
            #endregion
            ContactRecord newContact = new()
            {
                Ciudad = createrequest.Ciudad,
                FirstName = createrequest.FirstName,
                LastName = createrequest.LastName,
                Adress = createrequest.Adress,
                WorkPhoneNumber = createrequest.WorkPhoneNumber,
                PersonalPhoneNumber = createrequest.PersonalPhoneNumber,
                Company = createrequest.Company,
                Email = createrequest.Email,
                ProfileImage = Path.Combine("media", "profile_image", createrequest.ProfileImage.Name)

            };
            var contact = await _contextMySql.ContactRecords.AddAsync(newContact);
            await _contextMySql.SaveChangesAsync();

            return new OperationResponse<long>(contact.Entity.Id);

        }
        public async Task<OperationResponse<long>> Update(DtoEditContactRecord updaterequest)
        {
            #region Comprobaciones
            //Compruebo que exista el contacto

            ContactRecord contact = await _contextMySql
                                            .ContactRecords
                                            .FirstOrDefaultAsync(p => p.Id == updaterequest.ContactRecordId && !p.IsDeleted);

            if (contact == null) return new OperationResponse<long>(0, false, "El contacto buscado no existe");
            #endregion
            contact.Ciudad = updaterequest.Ciudad;
            contact.FirstName = updaterequest.FirstName;
            contact.LastName = updaterequest.LastName;
            contact.Adress = updaterequest.Adress;
            contact.WorkPhoneNumber = updaterequest.WorkPhoneNumber;
            contact.PersonalPhoneNumber = updaterequest.PersonalPhoneNumber;
            if (updaterequest.ProfileImage.IsNew) contact.ProfileImage = Path.Combine("media", "profile_image", updaterequest.ProfileImage.Name);
            contact.Company = updaterequest.Company;
            contact.Email = updaterequest.Email;

            _contextMySql.ContactRecords.Update(contact);
            await _contextMySql.SaveChangesAsync();

            return new OperationResponse<long>(updaterequest.Id);

        }

        public async Task<OperationResponse<DtoResponsePagination<DtoListPorCiudad>>> ListaCiudades(string filter, int pageSize, int page)
        {
            var query = _contextMySql
                        .ContactRecords
                        .AsNoTracking()
                        .Where(c => c.Ciudad.ToLower().Contains(filter ?? "") && !c.IsDeleted);

            var count = await query.CountAsync().ConfigureAwait(false);

            var list = (await query.OrderBy(c => c.Email + c.Ciudad)
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync()
                                    .ConfigureAwait(false))
                                    .Select(c => new DtoListPorCiudad()
                                    {
                                        Ciudad = c.Ciudad,
                                        FirstName = c.FirstName,
                                        WorkPhoneNumber = c.WorkPhoneNumber,
                                        LastName = c.LastName,
                                        Company = c.Company,
                                        PersonalPhoneNumber = c.PersonalPhoneNumber,
                                        Email = c.Email,
                                        Adress = c.Adress,
                                        ProfileImage = c.ProfileImage,
                                    });

            return new OperationResponse<DtoResponsePagination<DtoListPorCiudad>>(new DtoResponsePagination<DtoListPorCiudad>()
            {
                Data = list,
                PageSize = pageSize,
                TotalCount = count
            });

        }

        public async Task<OperationResponse<DtoGetByEmail>> GetByEmail(string email)
        {
            ContactRecord contact = await _contextMySql
                                    .ContactRecords
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Email == email && !c.IsDeleted);

            if (contact == null)
            {
                return new OperationResponse<DtoGetByEmail>(null, false, "El email que se ingresó no corresponde a un usuario registrado");
            }
            //Agregar retorno en caso de que encuentre
            var dto = new DtoGetByEmail()
            {
                Adress = contact.Adress,
                Email = contact.Email,
                Company = contact.Company,
                ProfileImage = contact.ProfileImage,
                PersonalPhoneNumber = contact.PersonalPhoneNumber,
                WorkPhoneNumber = contact.WorkPhoneNumber
            };
            return new OperationResponse<DtoGetByEmail>(dto);
        }

        public async Task<OperationResponse<bool>> DeleteContactRecord(long id)
        {
            ContactRecord  existeCR = await _contextMySql
                                    .ContactRecords
                                    .FirstOrDefaultAsync(c=>c.Id == id && !c.IsDeleted);

            if (existeCR == null)
            {
                return new OperationResponse<bool>(false, false, ("No existe el id del contacto que está buscando"));
            }

            var c = await _contextMySql.ContactRecords.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            c.IsDeleted = true;

            _contextMySql.ContactRecords.Update(c);
            await _contextMySql.SaveChangesAsync();

            return new OperationResponse<bool>(true);
        }
        public async Task<OperationResponse<DtoGetByNumber>> GetByNumber(string number)
        {
            ContactRecord contact = await _contextMySql
                        .ContactRecords
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => (c.PersonalPhoneNumber == number || c.WorkPhoneNumber == number) && !c.IsDeleted);

            if (contact == null)
            {
                return new OperationResponse<DtoGetByNumber>(null, false, "El número que se ingresó no corresponde a un usuario registrado");
            }
            //Agregar retorno en caso de que encuentre
            var dto = new DtoGetByNumber()
            {
                Adress = contact.Adress,
                Email = contact.Email,
                Company = contact.Company,
                ProfileImage = contact.ProfileImage,
                PersonalPhoneNumber = contact.PersonalPhoneNumber,
                WorkPhoneNumber = contact.WorkPhoneNumber
            };
            return new OperationResponse<DtoGetByNumber>(dto);
        }


    }
}