async function getMotoristaId(){
    const urlParams = new URLSearchParams(window.location.search);
    let res = await BuscarPorId(urlParams.get('id'));
    PreencherFormulario(res);
}

async function BuscarPorId(id){
    const options = {
        method: 'GET',
        headers:{'content-type':'aplication/json'}        

    };
    const req = await fetch ('https://localhost:44345/motorista/BuscarPorId?id='+id,options)
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
    let idMotorista = dadosForm.querySelector('#id-motorista');
    let idVeiculo = dadosForm.querySelector('#id-veiculo')
    let nome = dadosForm.querySelector('#nome');
    let cnh = dadosForm.querySelector('#cnh');
    let telefone= dadosForm.querySelector('#telefone');
    let placa = dadosForm.querySelector('#placa');
    
    idMotorista.value = json.value.resultado.idMotorista;
    idVeiculo.value = json.value.resultado.veiculo.idVeiculo;
    nome.value = json.value.resultado.nome;
    cnh.value = json.value.resultado.cnh;
    telefone.value = json.value.resultado.telefone;
    placa.value = json.value.resultado.veiculo.placa;
}

async function EnviarApi(viewmodel){
    const options = {
        method:'PUT',
        headers:{'content-type':'application/json'},
        body: JSON.stringify(viewmodel)
    };
    const req = await fetch('https://localhost:44345/motorista/atualizar',options)
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
    let idMotorista = parseInt(document.querySelector('#id-motorista').value);
    console.log(idMotorista);
    let idVeiculo = parseInt(document.querySelector('#id-veiculo').value);
    console.log(idVeiculo)
    let nome = document.querySelector('#nome').value;
    console.log(nome);
    let cnh = document.querySelector('#cnh').value;
    console.log(cnh);
    let telefone = document.querySelector('#telefone').value;
    console.log(telefone);
    let placa = document.querySelector('#placa').value;
    console.log(placa);

    let veiculo = {
        idVeiculo,
        placa,
        
    }
    let motorista = {
        idMotorista,
        nome,
        cnh,
        telefone,
        veiculo
    };
    let atualizarMotoristaEVeiculoViewModel = {
        motorista
    };
    const options = {    
        method: 'PUT', 
        headers:{'content-type': 'application/json'},       
        body: JSON.stringify(atualizarMotoristaEVeiculoViewModel) 
    };
    const req = await fetch('https://localhost:44345/motorista/Atualizar2',options)
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
getMotoristaId();