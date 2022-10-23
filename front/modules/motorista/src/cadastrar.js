async function SalvarMotorista(){
    let nome = document.querySelector('#nome').value;  
    console.log(nome);
    let cnh = document.querySelector('#cnh').value;  
    console.log(cnh);
    let telefone = document.querySelector('#telefone').value;  
    console.log(telefone);
    let placa = document.querySelector('#placa').value;
    console.log(placa);
    
    let veiculo = {
        placa,
    }
    let motorista = {
      nome,
      cnh,
      telefone,

    };
    let salvarVeiculoEMotoristaViewModel = {
      motorista,
      veiculo
    };
  
    console.log(salvarVeiculoEMotoristaViewModel);
  
    let response = await EnviarApi(salvarVeiculoEMotoristaViewModel);
    console.log(response);
  }
  
  //função para fazer uma request na api;
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
    const req =  await fetch('https://localhost:44345/motorista/salvar2', options)
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