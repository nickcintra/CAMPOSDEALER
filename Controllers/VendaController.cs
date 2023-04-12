using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TesteCamposDealer.DB;
using TesteCamposDealer.Response;

namespace TesteCamposDealer.Controllers
{
    public class VendaController : ApiController
    {
        /// <summary>
        /// Recupera a Venda por Id..
        /// </summary>
        /// <param name="idVenda"></param>
        /// <returns></returns>
        [System.Web.Http.ActionName("GetById")]
        public Venda GetById(int idVenda)
        {
            Venda vendaRet = null;

            DBTesteCamposDealerDataContext db = new DBTesteCamposDealerDataContext();
            db.DeferredLoadingEnabled = false;

            try
            {
                vendaRet = (from c in db.Venda
                            where c.idVenda == idVenda
                            select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vendaRet;
        }

        ///// <summary>
        ///// Recupera a Venda por Id..
        ///// </summary>
        ///// <param name="idCliente"></param>
        ///// <returns></returns>
        [System.Web.Http.ActionName("GetByIdClient")]
        public List<Venda> GroupByCliente(int idCliente)
        {
            List<Venda> listaVendas = null;

            DBTesteCamposDealerDataContext db = new DBTesteCamposDealerDataContext();
            db.DeferredLoadingEnabled = false;

            try
            {
                listaVendas = (from v in db.Venda
                             join c in db.Cliente
                             on v.idCliente equals c.idCliente
                             select v).Where(v => v.idCliente == idCliente).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaVendas;
        }

        /// <summary>
        /// Recupera todas as vendas
        /// </summary>
        /// <returns></returns>
        public List<Venda> GetAll()
        {
            List<Venda> lstVendaret = null;

            DBTesteCamposDealerDataContext db = new DBTesteCamposDealerDataContext();
            db.DeferredLoadingEnabled = false;

            try
            {
                lstVendaret = (from c in db.Venda
                               select c).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstVendaret;
        }


        /// <summary>
        /// Cadastra uma venda
        /// </summary>
        /// <param name="cliente"></param>
        public bool Post([FromBody] Venda vendaDTO)
        {
            DBTesteCamposDealerDataContext db = new DBTesteCamposDealerDataContext();
            db.DeferredLoadingEnabled = false;

            vendaDTO.Produto = (from c in db.Produto
                                where c.idProduto == vendaDTO.idProduto
                                select c).FirstOrDefault();
            vendaDTO.Cliente = (from c in db.Cliente
                                where c.idCliente == vendaDTO.idCliente
                                select c).FirstOrDefault();

            vendaDTO.vlrTotal = vendaDTO.qtdeProduto * vendaDTO.Produto.vlrProduto;


            try
            {
                db.Venda.InsertOnSubmit(vendaDTO);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Altera uma venda pelo Id
        /// </summary>
        /// <param name="idVenda"></param>
        /// <param name="vendaDTO"></param>
        [System.Web.Http.ActionName("PutById")]
        public Venda Put(int idVenda, [FromBody] Venda vendaDTO)
        {
            Venda vendaRet = null;

            DBTesteCamposDealerDataContext db = new DBTesteCamposDealerDataContext();
            db.DeferredLoadingEnabled = false;

            try
            {
                vendaRet = (from c in db.Venda
                            where c.idVenda == idVenda
                            select c).FirstOrDefault();

                //vendaRet.vlrProduto = vendaDTO.vlrProduto;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vendaRet;
        }

        /// <summary>
        /// Deleta uma venda pelo seu id
        /// </summary>
        /// <param name="idCliente"></param>
        [System.Web.Http.ActionName("DeleteById")]
        public bool Delete(int idVenda)
        {
            Venda vendaRet = null;

            DBTesteCamposDealerDataContext db = new DBTesteCamposDealerDataContext();
            db.DeferredLoadingEnabled = false;

            try
            {
                vendaRet = (from c in db.Venda
                            where c.idVenda == idVenda
                            select c).FirstOrDefault();

                db.Venda.DeleteOnSubmit(vendaRet);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
