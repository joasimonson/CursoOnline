import Curso from './page-model/curso'
import { getFullDate } from './util/func';

const curso = new Curso()

fixture('Curso').page(curso.url)

test('Deve criar novo curso', async t => {
    const nome = getFullDate("Curso teste: ");
    await t
        .typeText(curso.inputNome, nome)
        .typeText(curso.inputDescricao, nome)
        .typeText(curso.inputCargaHoraria, '20')
        .click(curso.selectPublicoAlvo)
        .click(curso.optionPublicoAlvo)
        .typeText(curso.inputValor, '1500')

    await t.click(curso.salvar)

    await t.expect(curso.tituloPagina.innerText).eql('Cursos')
})

test('Deve validar campos obrigatórios', async t => {
    await t.click(curso.salvar);

    await t
        .expect(curso.msgNome.withText('The Nome field is required.').count).eql(1)
        .expect(curso.msgDescricao.withText('The Descricao field is required.').count).eql(1)
        .expect(curso.msgCargaHoraria.withText('The CargaHoraria field is required.').count).eql(1)
        .expect(curso.msgValor.withText('The Valor field is required.').count).eql(1)
});