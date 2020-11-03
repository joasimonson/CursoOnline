import { Selector } from 'testcafe';
import Page from './page';

export default class Aluno extends Page {
    url: string
    inputNome: Selector
    inputCPF: Selector
    inputEmail: Selector
    selectPublicoAlvo: Selector
    optionPublicoAlvo: Selector

    msgNome: Selector
    msgCPF: Selector
    msgEmail: Selector

    salvar: Selector;

    constructor () {
        super();
        this.url = `${this.urlBase}/Aluno/Incluir`
        this.inputNome = Selector('[name="Nome"]')
        this.inputCPF = Selector('[name="CPF"]')
        this.inputEmail = Selector('[name="Email"]')
        this.selectPublicoAlvo = Selector('[name="PublicoAlvoId"]')
        this.optionPublicoAlvo = Selector('option[value="Todos"]')

        this.msgNome = Selector('#Nome-error')
        this.msgCPF = Selector('#CPF-error')
        this.msgEmail = Selector('#Email-error')

        this.salvar = Selector('#btn-salvar')
    }
}