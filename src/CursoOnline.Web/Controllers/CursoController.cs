using AutoMapper;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly SalvarCurso _inclusaoCurso;
        private readonly ICursoRepositorio _cursoRepositorio;

        public CursoController(IMapper mapper, SalvarCurso inclusaoCurso, ICursoRepositorio cursoRepositorio)
        {
            _mapper = mapper;
            _inclusaoCurso = inclusaoCurso;
            _cursoRepositorio = cursoRepositorio;
        }

        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();

            var cursosDTO = _mapper.Map<List<CursoDTO>>(cursos);

            return View(cursosDTO);
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            var listaPublicoAlvo = Enum.GetNames(typeof(PublicoAlvoEnum)).Select(p => new { id = p, value = p });

            ViewBag.ListaPublicoAlvo = new SelectList(listaPublicoAlvo, "id", "value");
            
            return View(new CursoDTO());
        }

        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var curso = _cursoRepositorio.ObterPorId(id);

            var cursoDTO = _mapper.Map<CursoDTO>(curso);

            return View(cursoDTO);
        }

        [HttpPost]
        public IActionResult Salvar(CursoDTO curso)
        {
            _inclusaoCurso.Salvar(curso);

            return RedirectToAction(nameof(Index));
        }
    }
}
