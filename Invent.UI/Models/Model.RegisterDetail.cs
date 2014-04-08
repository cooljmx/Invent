using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using Invent.Entities;
using Invent.Interfaces;
using InventUI.NHibernate;
using InventUI.Printer;
using NHibernate.Transform;
using NHibernate.Criterion;
using NHibernate;

namespace InventUI.Models
{
    public class ModelRegisterDetail<TGridItem, TEditableItem> : ModelCommon<TGridItem, TEditableItem>
        where TGridItem : IModelItem
        where TEditableItem : IModelItem
    {
        protected override IQueryOver<TEditableItem, TEditableItem> BuildQuery(ISession session)
        {
            Register register = null;
            RegisterDetails registerDetail = null;
            WorkPlaces workPlace = null;
            SpPlaces place = null;
            SpDetailTypes detailType = null;
            SpStatuses status = null;
            SpUsers molUser = null;
            SpUsers owner = null;
            SpUsers wpUser = null;
            ItemCommon item = null;

            return (IQueryOver<TEditableItem, TEditableItem>)session.QueryOver(() => register)
                .JoinAlias(() => register.Status, () => status)
                .JoinAlias(() => register.UserMaterial, () => molUser)
                .JoinAlias(() => register.UserOwner, () => owner)
                .Left.JoinAlias(() => register.Details, () => registerDetail)
                .Left.JoinAlias(() => registerDetail.DetailType, () => detailType)
                .Left.JoinAlias(() => registerDetail.WorkPlace, () => workPlace)
                .Left.JoinAlias(() => workPlace.Place, () => place)
                .Left.JoinAlias(() => workPlace.User, () => wpUser)
                .SelectList(list => list
                    .Select(c => c.Id).WithAlias(() => item.RegisterId)
                    .Select(c => registerDetail.Id).WithAlias(() => item.DetailId)
                    .Select(c => c.DateInput).WithAlias(() => item.DateInput)
                    .Select(c => c.DateStatus).WithAlias(() => item.DateStatus)
                    .Select(c => registerDetail.Id).WithAlias(() => item.DetailCount)
                    .Select(c => registerDetail.Name).WithAlias(() => item.DetailName)
                    .Select(c => detailType.Name).WithAlias(() => item.DetailTypeName)
                    .Select(c => c.InvName).WithAlias(() => item.InvName)
                    .Select(c => c.InvNumber).WithAlias(() => item.InvNumber)
                    .Select(c => registerDetail.Npp).WithAlias(() => item.Npp)
                    .Select(c => place.Name).WithAlias(() => item.Place)
                    .Select(c => workPlace.CustomKey).WithAlias(() => item.PlaceCustomKey)
                    .Select(c => status.Name).WithAlias(() => item.StatusName)
                    //.SelectGroup(c => status.CustomKey).WithAlias(() => item.StatusCustomKey)
                    .Select(c => status.ContentColor).WithAlias(() => item.StatusContentColor)
                    .Select(c => molUser.Name).WithAlias(() => item.UserMol)
                    .Select(c => owner.Name).WithAlias(() => item.UserOwner)
                    .Select(c => workPlace.Name).WithAlias(() => item.WorkPlace)
                    .Select(c => wpUser.Name).WithAlias(() => item.WorkPlaceUser)
                    .Select(c => registerDetail.Questions).WithAlias(() => item.Questions));
        }

        public void PrintFocusedItem()
        {
            try
            {
                var item = (ItemCommon) Convert.ChangeType(FocusedGridRow, typeof (ItemCommon));
                using (var registerItem = new ModelCardRegister())
                {
                    registerItem.Load(item);
                    var detail = registerItem.Details.First(x => x.Id == item.DetailId);
                    var parent = registerItem.RegisterItem;
                    IRegisterDetail registerDetail = new PrinterRegisterDetail()
                    {
                        Id = detail.Id,
                        Name = detail.Name,
                        Npp = detail.Npp,
                        Comment = detail.Comment,
                        Questions = detail.Questions > 0,
                        WorkPlace = new PrinterWorkPlace()
                        {
                            Id = detail.WorkPlace.Id != null ? detail.WorkPlace.Id.Value : 0,
                            Name = detail.WorkPlace.Name
                        },
                        Place = new PrinterPlace()
                        {
                            Id = detail.WorkPlace.Place.Id != null ? detail.WorkPlace.Place.Id.Value : 0,
                            Name = detail.WorkPlace.Place.Name
                        },
                        User = detail.WorkPlace.User != null
                            ? new PrinterUser()
                            {
                                Id = detail.WorkPlace.User.Id != null ? detail.WorkPlace.User.Id.Value : 0,
                                Name = detail.WorkPlace.User.Name,
                                Job = detail.WorkPlace.User.Job
                            }
                            : null,
                        DetailType = new PrinterDetailType()
                        {
                            Id = detail.DetailType.Id != null ? detail.DetailType.Id.Value : 0,
                            Name = detail.DetailType.Name
                        },
                        Register = new PrinterRegister()
                        {
                            Id = parent.Id,
                            InvName = parent.InvName,
                            InvNumber = parent.InvNumber,
                            DateInput = parent.DateInput,
                            DateStatus = parent.DateStatus,
                            Comment = parent.Comment,
                            UserMaterial = new PrinterUser()
                            {
                                Id = parent.UserMaterial.Id != null ? parent.UserMaterial.Id.Value : 0,
                                Name = parent.UserMaterial.Name,
                                Job = parent.UserMaterial.Job
                            },
                            UserOwner = new PrinterUser()
                            {
                                Id = parent.UserOwner.Id != null ? parent.UserOwner.Id.Value : 0,
                                Name = parent.UserOwner.Name,
                                Job = parent.UserOwner.Job
                            },
                            Status = new PrinterStatus()
                            {
                                Id = parent.Status.Id,
                                Name = parent.Status.Name
                            }
                        }
                    };
                    var printer = new StickPrinter();
                    printer.Print(registerDetail);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(string.Format("Ошибка печати: {0}", E.Message), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
