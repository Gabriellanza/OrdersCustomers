import { useEffect, useState } from 'react';
import { Plus, Pencil, Trash2, Search } from 'lucide-react';
import { useAxios } from '../../hooks/useAxios';
import { getClients, deleteClient } from '../../services/clientService';
import ClientForm from './ClientForm';
import Button from '../../components/ui/Button';
import Input from '../../components/ui/Input';
import Skeleton from '../../components/ui/Skeleton';
import toast from 'react-hot-toast';

const ClientList = () => {
    const api = useAxios();
    const [clients, setClients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editingClient, setEditingClient] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');

    const fetchClients = async () => {
        try {
            setLoading(true);
            const data = await getClients(api);
            if (Array.isArray(data)) {
                setClients(data);
            } else {
                setClients(data?.data || []);
            }
        } catch (error) {
            toast.error('Erro ao carregar clientes');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchClients();
    }, []);

    const handleDelete = async (id) => {
        if (window.confirm('Tem certeza que deseja excluir este cliente?')) {
            try {
                await deleteClient(api, id);
                toast.success('Cliente excluído');
                fetchClients();
            } catch (error) {
                toast.error('Erro ao excluir cliente');
            }
        }
    };

    const handleEdit = (client) => {
        setEditingClient(client);
        setIsModalOpen(true);
    };

    const handleCreate = () => {
        setEditingClient(null);
        setIsModalOpen(true);
    };

    const filteredClients = clients.filter(c =>
        c.nome.toLowerCase().includes(searchTerm.toLowerCase()) ||
        c.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
        c.cpfCnpj?.includes(searchTerm)
    );

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <h1 className="text-2xl font-bold text-gray-900">Clientes</h1>
                <Button onClick={handleCreate}>
                    <Plus size={20} />
                    Novo Cliente
                </Button>
            </div>

            <div className="bg-white p-4 rounded-lg shadow-sm border border-gray-200">
                <div className="mb-4">
                    <div className="relative max-w-md">
                        <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" size={20} />
                        <Input
                            placeholder="Procurar por Nome, Email ou CPF/CNPJ..."
                            className="pl-10"
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                        />
                    </div>
                </div>

                <div className="overflow-x-auto">
                    <table className="w-full text-left text-gray-600">
                        <thead className="bg-gray-50 text-gray-700 font-medium">
                            <tr>
                                <th className="p-3">Nome</th>
                                <th className="p-3">CPF/CNPJ</th>
                                <th className="p-3">Email</th>
                                <th className="p-3">Cidade/Estado</th>
                                <th className="p-3">Ativo</th>
                                <th className="p-3 text-right">Ações</th>
                            </tr>
                        </thead>
                        <tbody className="divide-y divide-gray-100">
                            {loading ? (
                                [...Array(5)].map((_, i) => (
                                    <tr key={i}>
                                        <td className="p-3"><Skeleton className="h-5 w-32" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-24" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-48" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-24" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-16" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-20 ml-auto" /></td>
                                    </tr>
                                ))
                            ) : filteredClients.length === 0 ? (
                                <tr>
                                    <td colSpan="6" className="p-8 text-center text-gray-500">
                                        Nenhum cliente encontrado.
                                    </td>
                                </tr>
                            ) : (
                                filteredClients.map((client) => (
                                    <tr key={client.id} className="hover:bg-gray-50 transition-colors">
                                        <td className="p-3 font-medium text-gray-900">{client.nome}</td>
                                        <td className="p-3 font-mono text-xs">{client.cpfCnpj}</td>
                                        <td className="p-3">{client.email}</td>
                                        <td className="p-3">
                                            {client.endereco ? `${client.endereco.cidade}/${client.endereco.estado}` : '-'}
                                        </td>
                                        <td className="p-3">
                                            <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${client.ativo
                                                    ? 'bg-green-100 text-green-700'
                                                    : 'bg-red-100 text-red-700'
                                                }`}>
                                                {client.ativo ? 'Ativo' : 'Inativo'}
                                            </span>
                                        </td>
                                        <td className="p-3 text-right space-x-2">
                                            <button
                                                onClick={() => handleEdit(client)}
                                                className="text-blue-600 hover:text-blue-800 p-1 rounded hover:bg-blue-50"
                                            >
                                                <Pencil size={18} />
                                            </button>
                                            <button
                                                onClick={() => handleDelete(client.id)}
                                                className="text-red-600 hover:text-red-800 p-1 rounded hover:bg-red-50"
                                            >
                                                <Trash2 size={18} />
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
            </div>

            <ClientForm
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSuccess={fetchClients}
                initialData={editingClient}
            />
        </div>
    );
};

export default ClientList;
