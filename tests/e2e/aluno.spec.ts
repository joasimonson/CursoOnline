import Aluno from './page-model/aluno'
import { getFullDate } from './util/func';

const aluno = new Aluno()

fixture('Aluno').page(aluno.url)

test('Deve criar novo aluno', async t => {
    const nome = getFullDate("Aluno teste: ");
    await t
        .typeText(aluno.inputNome, nome)
        .typeText(aluno.inputCPF, '948.857.370-87')
        .typeText(aluno.inputEmail, 'teste@teste.com')
        .click(aluno.selectPublicoAlvo)
        .click(aluno.optionPublicoAlvo)

    await t.click(aluno.salvar)

    await t.expect(aluno.tituloPagina.innerText).eql('Alunos')
})

test('Deve validar campos obrigatórios', async t => {
    await t.click(aluno.salvar);

    await t
        .expect(aluno.msgNome.withText('The Nome field is required.').count).eql(1)
        .expect(aluno.msgCPF.withText('The CPF field is required.').count).eql(1)
        .expect(aluno.msgEmail.withText('The Email field is required.').count).eql(1)
});