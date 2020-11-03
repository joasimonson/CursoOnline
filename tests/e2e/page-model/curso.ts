import { Selector } from 'testcafe';
import Page from './page';

export default class Curso extends Page {
    url: string
    inputNome: Selector
    inputDescricao: Selector
    inputCargaHoraria: Selector
    selectPublicoAlvo: Selector
    optionPublicoAlvo: Selector
    inputValor: Selector;

    msgNome: Selector
    msgDescricao: Selector
    msgCargaHoraria: Selector
    msgValor: Selector

    salvar: Selector;

    constructor () {
        super();
        this.url = `${this.urlBase}/Curso/Incluir`
        this.inputNome = Selector('[name="Nome"]')
        this.inputDescricao = Selector('[name="Descricao"]')
        this.inputCargaHoraria = Selector('[name="CargaHoraria"]')
        this.selectPublicoAlvo = Selector('[name="PublicoAlvoId"]')
        this.optionPublicoAlvo = Selector('option[value="Todos"]')
        this.inputValor = Selector('[name="Valor"]')

        this.msgNome = Selector('#Nome-error')
        this.msgDescricao = Selector('#Descricao-error')
        this.msgCargaHoraria = Selector('#CargaHoraria-error')
        this.msgValor = Selector('#Valor-error')

        this.salvar = Selector('#btn-salvar')
    }
}