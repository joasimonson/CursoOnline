using AutoMapper;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMatriculaRepositorio _matriculaRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly SalvarMatricula _salvarMatricula;

        public MatriculaController(
            IMapper mapper,
            IMatriculaRepositorio matriculaRepositorio,
            IAlunoRepositorio alunoRepositorio,
            ICursoRepositorio cursoRepositorio,
            SalvarMatricula criacaoDaMatricula)
        {
            _mapper = mapper;
            _matriculaRepositorio = matriculaRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _salvarMatricula = criacaoDaMatricula;
        }

        public IActionResult Index()
        {
            var matriculas = _matriculaRepositorio.Consultar();

            var matriculasDTO = _mapper.Map<List<MatriculaDTO>>(matriculas);

            return View(matriculasDTO);
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            var alunos = _alunoRepositorio.Consultar().Select(a => new
            {
                id = a.Id,
                value = a.Nome
            }).ToList();
            ViewBag.ListaAluno = new SelectList(alunos, "id", "value");

            var cursos = _cursoRepositorio.Consultar().Select(c => new
            {
                id = c.Id,
                value = c.Nome
            }).ToList();
            ViewBag.ListaCurso = new SelectList(cursos, "id", "value");

            return View(new MatriculaDTO());
        }

        [HttpPost]
        public IActionResult Salvar(MatriculaDTO matricula)
        {
            _salvarMatricula.Salvar(matricula);

            return RedirectToAction(nameof(Index));
        }
    }
}