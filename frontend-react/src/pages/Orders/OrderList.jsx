import { useEffect, useState } from 'react';
import { Plus, Pencil, Trash2, Search, Eye } from 'lucide-react';
import { useAxios } from '../../hooks/useAxios';
import { getOrders, deleteOrder } from '../../services/orderService';
import OrderForm from './OrderForm';
import Button from '../../components/ui/Button';
import Input from '../../components/ui/Input';
import Skeleton from '../../components/ui/Skeleton';
import toast from 'react-hot-toast';

const OrderList = () => {
    const api = useAxios();
    const [orders, setOrders] = useState([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isViewOnly, setIsViewOnly] = useState(false);
    const [editingOrder, setEditingOrder] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');

    const fetchOrders = async () => {
        try {
            setLoading(true);
            const data = await getOrders(api);
            if (Array.isArray(data)) {
                setOrders(data);
            } else {
                setOrders(data?.data || []);
            }
        } catch (error) {
            toast.error('Erro ao carregar pedidos');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchOrders();
    }, []);

    const handleDelete = async (id) => {
        if (window.confirm('Tem certeza que deseja deletar este pedido?')) {
            try {
                await deleteOrder(api, id);
                toast.success('Pedido deletado');
                fetchOrders();
            } catch (error) {
                toast.error('Erro ao deletar pedido');
            }
        }
    };

    const handleView = (order) => {
        setEditingOrder(order);
        setIsViewOnly(true);
        setIsModalOpen(true);
    };

    const handleEdit = (order) => {
        setEditingOrder(order);
        setIsViewOnly(false);
        setIsModalOpen(true);
    };

    const handleCreate = () => {
        setEditingOrder(null);
        setIsViewOnly(false);
        setIsModalOpen(true);
    };

    const filteredOrders = orders.filter(o =>
        (o.numeroOrdem?.toString() || '').includes(searchTerm) ||
        o.status?.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <h1 className="text-2xl font-bold text-gray-900">Ordens</h1>
                <Button onClick={handleCreate}>
                    <Plus size={20} />
                    Nova Ordem
                </Button>
            </div>

            <div className="bg-white p-4 rounded-lg shadow-sm border border-gray-200">
                <div className="mb-4">
                    <div className="relative max-w-md">
                        <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" size={20} />
                        <Input
                            placeholder="Procurar por Ordem"
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
                                <th className="p-3">Ordem #</th>
                                <th className="p-3">Status</th>
                                <th className="p-3">Total</th>
                                <th className="p-3">Data</th>
                                <th className="p-3 text-right">Ações</th>
                            </tr>
                        </thead>
                        <tbody className="divide-y divide-gray-100">
                            {loading ? (
                                [...Array(5)].map((_, i) => (
                                    <tr key={i}>
                                        <td className="p-3"><Skeleton className="h-5 w-20" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-24" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-20" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-24" /></td>
                                        <td className="p-3"><Skeleton className="h-5 w-20 ml-auto" /></td>
                                    </tr>
                                ))
                            ) : filteredOrders.length === 0 ? (
                                <tr>
                                    <td colSpan="5" className="p-8 text-center text-gray-500">
                                        Nenhuma ordem encontrada.
                                    </td>
                                </tr>
                            ) : (
                                filteredOrders.map((order) => (
                                    <tr key={order.id} className="hover:bg-gray-50 transition-colors">
                                        <td className="p-3 font-medium text-gray-900">#{order.numeroOrdem}</td>
                                        <td className="p-3">
                                            <span className="inline-flex px-2 py-1 text-xs font-semibold rounded-full bg-blue-100 text-blue-700">
                                                {order.descricaoStatus}
                                            </span>
                                        </td>
                                        <td className="p-3">${order.valorTotal?.toFixed(2)}</td>
                                        <td className="p-3 text-sm">
                                            {order.dataConclusao ? new Date(order.dataConclusao).toLocaleDateString() : '-'}
                                        </td>
                                        <td className="p-3 text-right space-x-2">
                                            {/* Only allow edit/delete if status allows it - assumed logic, but keeping default for now */}
                                            <button
                                                onClick={() => handleView(order)}
                                                className="text-blue-600 hover:text-blue-800 p-1 rounded hover:bg-blue-50"
                                            >
                                                <Eye size={18} />
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
            </div>

            <OrderForm
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSuccess={fetchOrders}
                initialData={editingOrder}
                viewOnly={isViewOnly}
            />
        </div>
    );
};

export default OrderList;
