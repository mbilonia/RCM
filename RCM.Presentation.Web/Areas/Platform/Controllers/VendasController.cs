﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCM.Application.ApplicationInterfaces;
using RCM.Application.ViewModels;
using RCM.Domain.DomainNotificationHandlers;
using RCM.Presentation.Web.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.Presentation.Web.Areas.Platform.Controllers
{
    [Authorize]
    [Authorize(Policy = "ActiveUser")]
    [Area("Platform")]
    public class VendasController : BaseController
    {
        private readonly IVendaApplicationService _vendaApplicationService;
        private readonly IClienteApplicationService _clienteApplicationService;
        private readonly IProdutoApplicationService _produtoApplicationService;

        public VendasController(IVendaApplicationService vendaApplicationService, IClienteApplicationService clienteApplicationService, IProdutoApplicationService produtoApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _vendaApplicationService = vendaApplicationService;
            _clienteApplicationService = clienteApplicationService;
            _produtoApplicationService = produtoApplicationService;
        }

        public IActionResult Index()
        {
            var list = _vendaApplicationService.Get();

            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var venda = _vendaApplicationService.GetById(id);
            if (venda == null)
                return NotFound();

            return View(venda);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendaViewModel venda)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(venda);
            }

            var commandResult = await _vendaApplicationService.Add(venda);

            if (commandResult.Success)
            {
                NotifyCommandResultSuccess();
                return RedirectToAction(nameof(Index));
            }
            else
                NotifyCommandResultErrors(commandResult.Errors);

            return View(venda);
        }

        public IActionResult Edit(Guid id)
        {
            var venda = _vendaApplicationService.GetById(id);
            if (venda == null)
                return NotFound();

            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VendaViewModel venda)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(venda);
            }

            var commandResult = await _vendaApplicationService.Update(venda);

            if (commandResult.Success)
            {
                NotifyCommandResultSuccess();
                return RedirectToAction(nameof(Index));
            }
            else
                NotifyCommandResultErrors(commandResult.Errors);

            return View(venda);
        }

        public IActionResult Delete(Guid id)
        {
            var venda = _vendaApplicationService.GetById(id);
            if (venda == null)
                return NotFound();

            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, VendaViewModel venda)
        {
            var commandResult = await _vendaApplicationService.Remove(venda);

            if (commandResult.Success)
            {
                NotifyCommandResultSuccess();
                return RedirectToAction(nameof(Index));
            }
            else
                NotifyCommandResultErrors(commandResult.Errors);

            return View(venda);
        }

        public IActionResult AttachProduto(Guid id)
        {
            VendaProdutoViewModel vendaProduto = new VendaProdutoViewModel()
            {
                VendaId = id
            };

            return View(vendaProduto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttachProduto(VendaProdutoViewModel vendaProduto)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(vendaProduto);
            }

            var commandResult = await _vendaApplicationService.AttachProduto(vendaProduto);

            if (commandResult.Success)
            {
                NotifyCommandResultSuccess();
                return RedirectToAction(nameof(Details), new { id = vendaProduto.ProdutoId });
            }
            else
                NotifyCommandResultErrors(commandResult.Errors);

            return View(vendaProduto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProduto(Guid vendaId, Guid produtoId)
        {
            var commandResult = await _vendaApplicationService.RemoveProduto(vendaId, produtoId);

            if (commandResult.Success)
                NotifyCommandResultSuccess();
            else
                NotifyCommandResultErrors(commandResult.Errors);

            return RedirectToAction(nameof(Details), new { id = vendaId });
        }

        public JsonResult GetClientes()
        {
            return Json(_clienteApplicationService.Get()
                .OrderBy(c => c.Nome));
        }

        public JsonResult GetProdutos()
        {
            return Json(_produtoApplicationService.Get()
                .OrderBy(c => c.Nome));
        }
    }
}