using AutoMapper;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly SalvarAluno _salvarAluno;
        private readonly IAlunoRepositorio _alunoRepositorio;

        public AlunoController(IMapper mapper, SalvarAluno inclusaoAluno, IAlunoRepositorio alunoRepositorio)
        {
            _mapper = mapper;
            _salvarAluno = inclusaoAluno;
            _alunoRepositorio = alunoRepositorio;
        }

        public IActionResult Index()
        {
            var alunos = _alunoRepositorio.Consultar();

            var alunosDTO = _mapper.Map<List<AlunoDTO>>(alunos);

            return View(alunosDTO);
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            var listaPublicoAlvo = Enum.GetNames(typeof(PublicoAlvoEnum)).Select(p => new { id = p, value = p });

            ViewBag.ListaPublicoAlvo = new SelectList(listaPublicoAlvo, "id", "value");

            return View(new AlunoDTO());
        }

        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var aluno = _alunoRepositorio.ObterPorId(id);

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return View(alunoDTO);
        }

        [HttpPost]
        public IActionResult Salvar(AlunoDTO aluno)
        {
            _salvarAluno.Salvar(aluno);

            return RedirectToAction(nameof(Index));
        }
    }
}
