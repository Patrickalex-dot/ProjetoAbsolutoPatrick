async function getPedidoId(){
    const urlParams = new URLSearchParams(window.location.search);
    let res = await BuscarPorId(urlParams.get('id'));
    PreencherFormulario(res);
}
async function Atualizar(){
    let idcliente = parseInt(document.querySelector('#listagem-clientes').value);
    let idPedido = parseInt(document.querySelector('#id-pedido').value);
    console.log(idcliente);
    let idproduto = parseInt(document.querySelector('#listagem-produtos').value);
    console.log(idproduto);
    let idmotorista = parseInt(document.querySelector('#listagem-motoristas').value);
    console.log(idmotorista);
    let situacao = document.querySelector('#situacao').value;
    console.log(situacao);
    let valorProduto = document.querySelector('#valorProduto').value;
    let dataEntrega = document.querySelector('#data-hora-entrega').value;
    let tipoPagamento = document.querySelector('#tipopagamento').value;
   
    console.log(valorProduto);
    
    
    
    let pedido = {
      id: idPedido,  
      idcliente : parseInt(idcliente),
      idmotorista : parseInt(idmotorista) ,
      idproduto: parseInt(idproduto),
      situacao,
      valorTotal: parseFloat(valorProduto),
      dataHoraEntrega : dataEntrega,
      idPagamento: parseInt(tipoPagamento) 
    };
    let atualizarPedidoViewModel = {
        atualizar : pedido
    };
    const options = {    
        method: 'PUT', 
        headers:{'content-type': 'application/json'},       
        body: JSON.stringify(atualizarPedidoViewModel) 
    };
    const req = await fetch('https://localhost:44345/pedido/Atualizar2',options)
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
async function BuscarPorId(id){
    const options = {
        method: 'GET',
        headers:{'content-type':'aplication/json'}        

    };
    const req = await fetch ('https://localhost:44345/pedido/BuscarPorId?id='+id,options)
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
    await PreencherProduto(json.value.resultado.idProduto);
    await PreencherClientes(json.value.resultado.idCliente);
    await PreencherMotorista(json.value.resultado.idMotorista);
    let dadosForm = document.querySelector('#form');
    let id = dadosForm.querySelector('#id-pedido');
    let selectSituacao = document.querySelector('#situacao');
    let dataHoraEntrega = document.querySelector('#data-hora-entrega');
    let valorPedidoInput = document.querySelector('#valorProduto');
    let tipoPagamento = document.querySelector('#tipopagamento');
    tipoPagamento.value = json.value.resultado.idPagamento;
    valorPedidoInput.value = json.value.resultado.valorTotal;
    dataHoraEntrega.value = json.value.resultado.dataHoraEntrega;
    selectSituacao.value = json.value.resultado.situacao;
    id.value = json.value.resultado.idPedido;
}
async function CapturarDadosPedido(){
    let idcliente = document.querySelector('#listagem-clientes').value;  
    console.log(idcliente);
    let  idmotorista = document.querySelector('#listagem-motoristas').value;  
    console.log(idmotorista);
    let produto = document.querySelector('#listagem-produtos').value;  
    console.log(produto);
    let situacao = document.querySelector('#situacao').value;
    console.log(situacao);
    let valorProduto = document.querySelector('#valorProduto').value;
    let dataEntrega = document.querySelector('#data-hora-entrega').value;    

    console.log(valorProduto);
    
    
    
    let pedido = {
      idcliente : parseInt(idcliente),
      idmotorista : parseInt(idmotorista) ,
      idproduto: parseInt(produto),
      situacao,
      valorTotal: parseFloat(valorProduto),
      dataHoraEntrega : dataEntrega,
      idPagamento: 1 
    };
    let salvarPedidoViewModel = {
      pedido
    };
  
    console.log(salvarPedidoViewModel);
  
    let response = await EnviarApi(salvarPedidoViewModel);
    console.log(response);
  }

async function EnviarApi(viewmodel){    
    //opções/dados para fazer a request;
    const options = {
      //método, se é um post, get etc..
      method: 'POST',
      headers:{'content-type':'application/json'},
      //converte o objeto em um Json real;
      body:JSON.stringify(viewmodel) 
    };
  
    //TODO: mudar a url para o seu localhost.
    const req = await fetch('https://localhost:44345/pedido/save2', options)
    //caso a request dê certo, retornará a resposta;
      .then(response => {
          return response.json();
      }) 
    //caso dê erro, irá retornar o erro e mostrar no console
      .catch(erro => {
          console.log(erro);
          return erro;
      });
      if(req.value.sucesso){
           alert(req.value.mensagem);
           voltar();
      }

      return req;
  }
function voltar(){
   window.location.href ='./listar.html';
}

async function ListarMotoristas(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/motorista/BuscarTodos',options)
    .then(response=>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req;
}
async function ListarProdutos(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/produto/BuscarTodos',options)
    .then(response=>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req;
}


async function PreencherMotorista(idMotorista){
    let res = await ListarMotoristas();
    if(res.value.resultado.length > 0){
        let selectMotoristas = document.querySelector('#listagem-motoristas');
        res.value.resultado.forEach(element => {
            let option = document.createElement('option');
            option.value = element.idMotorista;
            option.text = element.nome;
            if(element.idMotorista == idMotorista){
                option.selected = true;
            }
            selectMotoristas.appendChild(option);
        });
    }
}
async function ListarClientes(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/cliente/BuscarTodos2',options)
    .then(response=>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req;
}

async function PreencherClientes(idCliente){
    let res = await ListarClientes();
    if(res.value.clientes.length > 0){
        let selectClientes = document.querySelector('#listagem-clientes');
        res.value.clientes.forEach(element => {
            let option = document.createElement('option');
            option.value = element.idCliente;
            option.text = element.nome;
            if(element.idCliente == idCliente){
                option.selected = true;
            }
            selectClientes.appendChild(option);               
        });
    }
}

async function PreencherProduto(idProduto){
    let res = await ListarProdutos();
    if(res.value.resultado.length > 0){
        let selectProdutos = document.querySelector('#listagem-produtos');
        res.value.resultado.forEach(element =>{
            let option = document.createElement('option');
            option.value = element.idProduto;
            option.text = element.descricao;
            if(element.idProduto == idProduto){
                option.selected = true;
            }
            selectProdutos.appendChild(option);
        });
    }
}
(async() =>{
    await getPedidoId();
})()