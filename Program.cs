using Store.Database;
using Store.Models;
using Store.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var dbsetup = new DatabaseSetup();
        var customerRepository = new CustomerRepository();
        var orderRepository = new OrderRepository();
        var orderItemRepository = new OrderItemsRepository();
        var productRepository =  new ProductRepository();
        var sellerRepository = new SellerRepository();

        var modelName = args[0];
        var modelAction = args[1];

        if (modelName.ToLower() == "cliente")
        {
            if (modelAction.ToLower() == "listar")
            {
                Console.WriteLine("Listar Clientes");
                Console.WriteLine("Id Cliente   Nome do Cliente    Endereço do Cliente      Cidade      Código Postal   Uf   Ie");
                foreach (var customer in customerRepository.GetAll())
                {
                    Console.WriteLine($"{customer.Id,-12} {customer.Name,-24} {customer.Address,-11} {customer.City,-11}  {customer.ZipCode,-15} {customer.UF,-9} {customer.IE}");
                }
            }
            else if (modelAction.ToLower() == "inserir")
            {
                Console.WriteLine("Insira um novo Cliente");
                Console.Write("Digite o id do cliente            : ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite a nome do cliente        : ");
                string name = Console.ReadLine();
                Console.Write("Digite o endereço do cliente      : ");
                string address = Console.ReadLine();
                Console.Write("Digite a cidade do cliente        : ");
                string city = Console.ReadLine();
                Console.Write("Digite o código postal do cliente : ");
                string zipcode = Console.ReadLine();
                Console.Write("Digite o uf do cliente          : ");
                string uf = Console.ReadLine();
                Console.Write("Digite o ie do cliente      : ");
                string ie = Console.ReadLine();
                var customer = new Customer(id, name, address, city, zipcode, uf, ie);
                customerRepository.Save(customer);

            }
            else if (modelAction.ToLower() == "apresentar")
            {
                Console.WriteLine("Apresentar Cliente");
                Console.Write("Digite o id do cliente : ");
                var id = Convert.ToInt32(Console.ReadLine());

                if (customerRepository.ExitsById(id))
                {
                    var customer = customerRepository.GetById(id);
                    Console.WriteLine($"{customer.Id,-12} {customer.Name,-24} {customer.Address,-11} {customer.City,-11}  {customer.ZipCode,-15} {customer.UF,-9} {customer.IE}");
                }
                else
                {
                    Console.WriteLine($"O Cliente com Id {id} não existe.");
                }
            }
        }
        else if (modelName.ToLower() == "pedido")
        {
            if (modelAction.ToLower() == "listar")
            {
                Console.WriteLine("Listar Pedidos");
                Console.WriteLine("Nro Pedido   Id Empregado   Data do Pedido   Peso      Codigo da Transportadora   Id do Cliente");

                foreach (var order in orderRepository.GetAll())
                    Console.WriteLine($"{order.OrderId,-12} {order.OrderDate,-14} {order.Deadline,-16} {order.CustomerId} {order.SellerId}");
            }
            else if (modelAction.ToLower() == "inserir")
            {
                Console.WriteLine("Insira novo pedido");
                Console.Write("Digite o id do pedido                       : ");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o código do cliente do pedido        : ");
                var customerId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o código da transportadora do pedido : ");
                var sellerId = Convert.ToInt32(Console.ReadLine());
                var order = new Order(id, customerId, sellerId);
                orderRepository.Save(order);
            }
            else if (modelAction.ToLower() == "apresentar")
            {
                Console.WriteLine("Apresentar Pedido");
                Console.Write("Digite o id do pedido : ");
                var id = Convert.ToInt32(Console.ReadLine());
                if (orderRepository.ExitsById(id))
                {
                    var order = orderRepository.GetById(id);
                    Console.WriteLine($"{order.OrderId,-12} {order.OrderDate,-14} {order.Deadline,-16} {order.CustomerId} {order.SellerId}");
                }
                else
                {
                    Console.WriteLine($"O pedido com Id {id} não existe.");
                }
            }
            else if (modelName.ToLower() == "vendedor")
            {
                if (modelAction.ToLower() == "listar")
                {
                    Console.WriteLine("Listar Vendedor");
                    Console.WriteLine("Cod Vendedor Nome   Salario fixo   FaixaComissao");
                    foreach (var seller in sellerRepository.GetAll())
                    {
                        Console.WriteLine($"{seller.SellerId,-12} {seller.Name,-14} {seller.FixedIncome,-16} {seller.CommissionRank,-9}");
                    }
                }

                if (modelAction.ToLower() == "inserir")
                {
                    Console.WriteLine("Inserir Vendedor");
                    Console.Write("Digite o cod do vendedor: ");
                    int sellerId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Digite o nome do vendedor: ");
                    string name = Console.ReadLine();
                    Console.Write("Digite o salário fixo: ");
                    decimal fixedIncome = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Digite a faixa de comissão: ");
                    string commissionRank = Console.ReadLine();

                    var seller = new Seller(sellerId, name, fixedIncome, commissionRank);
                    sellerRepository.Save(seller);
                }

                if (modelAction.ToLower() == "apresentar")
                {
                    Console.WriteLine("Apresentar vendedor");
                    Console.Write("Digite o cod do vendedor: ");
                    var sellerId = Convert.ToInt32(Console.ReadLine());

                    if (sellerRepository.ExistById(sellerId))
                    {
                        var vendedor = vendedorRepository.GetByIdVendedor(codVendedor);
                        Console.WriteLine($"{vendedor.Codvendedor}, {vendedor.Nome}, {vendedor.SalarioFixo}, {vendedor.FaixaComissao}");
                    }
                    else
                    {
                        Console.WriteLine($"O pedido com cod {codVendedor} não existe.");
                    }
                }
            }
        if (modelNome == "Produto")
        {
            if (modelAcao == "Listar")
            {
                Console.WriteLine("Listar Produto");
                Console.WriteLine("Cod Produto            Descrição                                       Valor Unitário");
                foreach (var produto in produtoRepository.GetAll())
                {
                    Console.WriteLine($"{produto.CodProduto,-22} {produto.Descricao,-43} {produto.ValorUnitario,+10}");
                }
            }

            if (modelAcao == "Inserir")
            {
                Console.WriteLine("Inserir Produto");

                int codproduto;
                string descricao;
                decimal valorunitario;

                Console.Write("Digite o cod do produto: ");
                codproduto = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite a descrição do produto: ");
                descricao = Console.ReadLine();
                Console.Write("Digite o valor unitário do produto: ");
                valorunitario = Convert.ToDecimal(Console.ReadLine());

                var produto = new Produto(codproduto, descricao, valorunitario);
                produtoRepository.Save(produto);
            }

            if (modelAcao == "Apresentar")
            {
                Console.WriteLine("Apresentar Produto");
                Console.Write("Digite o cod do Produto: ");
                var codProduto = Convert.ToInt32(Console.ReadLine());

                if (produtoRepository.ExistByIdProduto(codProduto))
                {
                    var produto = produtoRepository.GetByIdProduto(codProduto);
                    Console.WriteLine($"{produto.CodProduto}, {produto.Descricao}, {produto.ValorUnitario}");
                }
                else
                {
                    Console.WriteLine($"O produto com cod {codProduto} não existe.");
                }
            }
        }
        if (modelNome == "ItensPedido")
        {
            if (modelAcao == "Listar")
            {
                Console.WriteLine("Listar Itenspedido");
                Console.WriteLine("CodItenspedido itempedidocodpedido   itempedidocodproduto       Quantidade");
                foreach (var itenspedido in itensPedidoRepository.GetAll())
                {
                    Console.WriteLine($"{itenspedido.CodItemPedido,+5} {itenspedido.ItemPedidoCodPedido,+20} {itenspedido.ItemPedidoCodProduto,+20} {itenspedido.Quantidade,+20}");
                }
            }

            if (modelAcao == "Inserir")
            {
                Console.WriteLine("Inserir Itens do pedido");

                int coditempedido;
                int itempedidocodpedido;
                int itempedidocodproduto;
                int quantidade;

                Console.Write("Digite o cod do item do pedido: ");
                coditempedido = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o código do pedido do item do pedido: ");
                itempedidocodpedido = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o código do produto do item do pedido: ");
                itempedidocodproduto = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite a quantidade do item do pedido");
                quantidade = Convert.ToInt32(Console.ReadLine());

                var itenspedido = new ItensPedido(coditempedido, itempedidocodpedido, itempedidocodproduto, quantidade);
                itensPedidoRepository.Save(itenspedido);
            }

            if (modelAcao == "Apresentar")
            {
                Console.WriteLine("Apresentar ItensPedido");
                Console.Write("Digite o cod do ItensPedido: ");
                var codItensPedido = Convert.ToInt32(Console.ReadLine());

                if (itensPedidoRepository.ExistByIdItensPedido(codItensPedido))
                {
                    var itenspedido = itensPedidoRepository.GetByIdItensPedido(codItensPedido);
                    Console.WriteLine($"{itenspedido.CodItemPedido}, {itenspedido.ItemPedidoCodPedido}, {itenspedido.ItemPedidoCodProduto}{itenspedido.Quantidade}");
                }
                else
                {
                    Console.WriteLine($"O produto com cod {codItensPedido} não existe.");
                }
            }
        }
    }
}
