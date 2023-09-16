
//Gerenciador de tarefas
class Program
{
    enum Opcao
    {
        Tarefas = 1,
        Adicionar,
        Finalizar,
        Apagar
    };

    class Tarefa
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }

        public Tarefa(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            Finalizada = false;
            ID = Guid.NewGuid();
        }
    }

    static List<Tarefa> listaDeTarefas = new List<Tarefa>();

    static void Main(String[] args)
    {
        Init(true);
    }
    static void Init(bool primeiraVez)
    {   
        if(primeiraVez) {
            Console.WriteLine("Bem-Vindo ao seu gerenciador de Tarefas!!!\n");
        }
        else
        {
            Console.WriteLine("\nRealizar outra operação?\n");
            Console.WriteLine("1 - SIM\n2 - NÂO\n");
            string fazerNovaOperação = Console.ReadLine();

            if(fazerNovaOperação != "1")
            {
                Environment.Exit(0);
            }
        }
        Console.WriteLine("O que deseja realizar?\n");
        Console.WriteLine("1 - Listar tarefas\n2 - Adicionar uma nova tarefa\n3 - Finalizar uma tarefa\n4 - Apagar uma tarefa");

        string opcaoSelecionada = Console.ReadLine();
        Opcao OPÇAO;

        //validar a opção selecionada
        bool opcaoEValida = opcaoValida(opcaoSelecionada);
        if (opcaoEValida)
        {
            OPÇAO = (Opcao)int.Parse(opcaoSelecionada);
            realizarTask(OPÇAO);

        }
        else
        {
            Console.WriteLine("Não consegui entender oque você solicitou!");
        }

    }

    static bool opcaoValida(string OpcaoSelecionada)
    {
       if(OpcaoSelecionada == "1" || OpcaoSelecionada == "2" || OpcaoSelecionada == "3" || OpcaoSelecionada == "4")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    static void realizarTask(Opcao OPÇAO)
    {
        switch (OPÇAO)
        {
            case Opcao.Tarefas:
                listarTarefas();
                Init(false);
                break;
            case Opcao.Adicionar:
                adicionarTarefa();
                Init(false);
                break;
            case Opcao.Finalizar:
                finalizarTarefa();
                Init(false);
                break;
            case Opcao.Apagar:
                removerTarefa();
                Init(false);
                break;
            default:
                Init(false);
                break;
        }
    }
    static void listarTarefas()
    {
        if (listaDeTarefas.Count > 0)
        {
            foreach (Tarefa tarefa in listaDeTarefas)
            {
                Console.WriteLine("\n----------/ TAREFA - "+ tarefa.ID +" /----------\n");
                Console.WriteLine("ID da Tarefa: " + tarefa.ID);
                Console.WriteLine("Nome da Tarefa: " + tarefa.Nome);
                Console.WriteLine("Descrição da Tarefa: " + tarefa.Descricao);
                Console.WriteLine("Status da Tarefa: " + (tarefa.Finalizada ? "FINALIZADA" : "PENDENTE"));
            }
        }
        else
        {
            Console.WriteLine("Você não possui tarefas cadastradas!");
        }
       
    }
    static void adicionarTarefa()
    {
        Console.WriteLine("\n----------/ Adicionar nova Tarefa /----------\n");
        string nomeDaTarefa;
        while (true) 
        {
            Console.Write("Nome da Tarefa: ");
            nomeDaTarefa = Console.ReadLine();

            if (!string.IsNullOrEmpty(nomeDaTarefa))
            {
                break; 
            }
            else
            {
                Console.WriteLine("\nNome inválido. Tente novamente.\n");
            }
        }
        string descricaoDaTarefa;
        while (true)
        {
            Console.Write("\nDescrição da Tarefa: ");
            descricaoDaTarefa = Console.ReadLine();

            if (!string.IsNullOrEmpty(descricaoDaTarefa))
            {
                break;
            }
            else
            {
                Console.WriteLine("\nDescrição inválida. Tente novamente.\n");
            }
        }

        listaDeTarefas.Add(new Tarefa(nomeDaTarefa, descricaoDaTarefa));

        Console.WriteLine("\n----------/ Tarefa adicionada com sucesso ;-) /----------\n");
    }
    static void finalizarTarefa()
    {
        if(listaDeTarefas.Count > 0)
        {
            Console.WriteLine("\n----------/ Finalizar Tarefa /----------\n");

            listarTarefas();
            string idDaTarefa;
            while (true)
            {
                Console.Write("\nID da tarefa que deseja finalizar: ");
                idDaTarefa = Console.ReadLine();

                
                if (!string.IsNullOrEmpty(idDaTarefa) && Guid.TryParse(idDaTarefa, out Guid guidValue))
                {
                    Tarefa tarefaAchadaPeloId = listaDeTarefas.Find(tarefa => tarefa.ID == guidValue);  

                    if (string.IsNullOrEmpty(tarefaAchadaPeloId?.Nome))
                    {
                        Console.WriteLine("\nNão existe nenhuma tarefa com esse ID. Tente novamente.\n");

                    }
                    else
                    {
                        if (tarefaAchadaPeloId.Finalizada)
                        {
                            Console.WriteLine("\n----------/ Tarefa " + tarefaAchadaPeloId.ID + " já esta finalizada! /----------\n");
                            break;
                        }
                        else
                        {
                            tarefaAchadaPeloId.Finalizada = true;
                            Console.WriteLine("\n----------/ Tarefa " + tarefaAchadaPeloId.ID + " finalizada com sucesso ;-) /----------\n");

                        }

                        break;
                    }


                }
                else
                {
                    Console.WriteLine("\nID inválido. Tente novamente.\n");
                }
            }
        }
        else
        {
            Console.WriteLine("Você não possui tarefas cadastradas!");
        }
    }
    static void removerTarefa()
    {
        if (listaDeTarefas.Count > 0)
        {
            Console.WriteLine("\n----------/ Excluir Tarefa /----------\n");

            listarTarefas();
            string idDaTarefa;
            while (true)
            {
                Console.Write("\nID da tarefa que deseja excluir: ");
                idDaTarefa = Console.ReadLine();


                if (!string.IsNullOrEmpty(idDaTarefa) && Guid.TryParse(idDaTarefa, out Guid guidValue))
                {
                    Tarefa tarefaAchadaPeloId = listaDeTarefas.Find(tarefa => tarefa.ID == guidValue);

                    if (string.IsNullOrEmpty(tarefaAchadaPeloId?.Nome))
                    {
                        Console.WriteLine("\nNão existe nenhuma tarefa com esse ID. Tente novamente.\n");

                    }
                    else
                    {
                        listaDeTarefas.Remove(tarefaAchadaPeloId);
                        Console.WriteLine("\n----------/ Tarefa " + tarefaAchadaPeloId.ID + " deletada com sucesso ;-) /----------\n");
                        break;
                    }


                }
                else
                {
                    Console.WriteLine("\nID inválido. Tente novamente.\n");
                }
            }
        }
        else
        {
            Console.WriteLine("Você não possui tarefas cadastradas!");
        }
    }
}