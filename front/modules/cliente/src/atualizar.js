async function getPessoaId(){
    const urlParams = new URLSearchParams(window.location.search);
    let res = await BuscarPorId(urlParams.get('id'));
    PreencherFormulario(res);
}

async function BuscarPorId(id){
    const options = {
        method: 'GET',
        headers:{'content-type':'aplication/json'}        

    };
    const req = await fetch ('https://localhost:44345/cliente/BuscarPorId?id='+id,options)
    .then(response =>{
        return response.json()        
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req
}
async function PreencherFormulario(json){

    let dadosForm = document.querySelector('#form');
    let id = dadosForm.querySelector('#id-cliente');
    let nome = dadosForm.querySelector('#nomeCompleto');
    let cpf = dadosForm.querySelector('#cpf');
    let nascimento = dadosForm.querySelector('#nascimento');
    let telefone= dadosForm.querySelector('#telefone');
    let rua = dadosForm.querySelector('#rua');
    let numero = dadosForm.querySelector('#numero');
    let bairro = dadosForm.querySelector('#bairro');
    let cidade = dadosForm.querySelector('#cidade');
    let complemento = dadosForm.querySelector('#complemento');

    id.value = json.value.resultado.idCliente;
    nome.value = json.value.resultado.nome;
    cpf.value = json.value.resultado.cpf;
    nascimento.valueAsDate = convertToDate(json.value.resultado.dataNascimento);
    telefone.value = json.value.resultado.telefone;
    rua.value = json.value.resultado.rua;
    numero.value = json.value.resultado.numero;
    bairro.value = json.value.resultado.bairro;
    cidade.value = json.value.resultado.cidade;
    complemento.value = json.value.resultado.referencia;
}
async function EnviarApi(viewmodel){
    const options = {
        method:'PUT',
        headers:{'content-type':'application/json'},
        body: JSON.stringify(viewmodel)
    };
    const req = await fetch('https://localhost:44345/cliente/atualizar',options)
    .then(response =>{
        response.text()
        .then(data =>{
            return data;
        });
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req
}
async function Atualizar(){
    let id = parseInt(document.querySelector('#id-cliente').value);
    console.log(id);
    let nome = document.querySelector('#nomeCompleto').value;
    console.log(nome);
    let cpf = document.querySelector('#cpf').value;
    console.log(cpf);
    let nascimento = document.querySelector('#nascimento').value;
    console.log(nascimento);
    let telefone = document.querySelector('#telefone').value;
    console.log(telefone);
    let rua = document.querySelector('#rua').value;
    console.log(rua);
    let numero = document.querySelector('#numero').value;
    console.log(numero);
    let bairro = document.querySelector('#bairro').value;
    console.log(bairro);
    let cidade = document.querySelector('#cidade').value;
    console.log(cidade);
    let complemento = document.querySelector('#complemento').value;

    let cliente = {
        idCliente: id,
        nome,
        cpf,
        dataNascimento : nascimento,
        telefone,
        rua,
        numero,
        bairro,
        cidade,
        referencia: complemento,
        tipocontato: "WhatsApp"
    };
    let atualizarClienteViewModel = {
        atualizar : cliente
    };
    const options = {    
        method: 'PUT', 
        headers:{'content-type': 'application/json'},       
        body: JSON.stringify(atualizarClienteViewModel) 
    };
    const req = await fetch('https://localhost:44345/cliente/Atualizar2',options)
    .then (response =>{
        return response.json();

    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    if(req.value.sucesso){
        alert(req.value.mensagem);
        voltar();
    }
    else{
        alert(req.mensagem);
    }
}
function voltar(){
    window.location.href ='./listar.html';
}
function convertToDate(data){
    var arrayDate =data.split('/');
    var dt = new Date(arrayDate[2], arrayDate[1], arrayDate[0]);
    return dt;
}
getPessoaId();