#language:pt-BR
Funcionalidade: Eu com usuario
	Quero validar os elementos de acessibilidade na tela

@teste
Cenario: Validar os elementos de acessibilidade na tela
	Dado Que eu inicio o Browser em acessibilidade com o link "https://itau.com.br"
	E ativo o Nvda
	E inicio o focu em "serviços"	
	Quando valido os elementos da tela
	Entao Fecho o navegador
