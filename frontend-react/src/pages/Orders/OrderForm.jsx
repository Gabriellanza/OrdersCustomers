import { useState, useEffect } from 'react';
import { Plus, Trash2 } from 'lucide-react';
import { useAxios } from '../../hooks/useAxios';
import { createOrder, updateOrder } from '../../services/orderService';
import { getClients } from '../../services/clientService';
import Modal from '../../components/ui/Modal';
import Input from '../../components/ui/Input';
import Button from '../../components/ui/Button';
import toast from 'react-hot-toast';

const OrderForm = ({ isOpen, onClose, onSuccess, initialData, viewOnly }) => {
    const api = useAxios();
    const [loading, setLoading] = useState(false);
    const [clients, setClients] = useState([]);

    const [formData, setFormData] = useState({
        clienteId: '',
        items: []
    });

    useEffect(() => {
        const loadClients = async () => {
            try {
                const data = await getClients(api);
                setClients(Array.isArray(data) ? data : (data?.data || []));
            } catch (error) {
                console.error("Erro ao carregar clientes", error);
            }
        };
        if (isOpen) {
            loadClients();
        }
    }, [isOpen, api]);

    useEffect(() => {
        if (initialData) {
            setFormData({
                clienteId: initialData.clienteId || '',
                items: initialData.items || []
            });
        } else {
            setFormData({
                clienteId: '',
                items: []
            });
        }
    }, [initialData, isOpen]);

    const handleAddItem = () => {
        setFormData(prev => ({
            ...prev,
            items: [...prev.items, { nomeProduto: '', quantidade: 1, valorUnitario: 0 }]
        }));
    };

    const handleRemoveItem = (index) => {
        setFormData(prev => ({
            ...prev,
            items: prev.items.filter((_, i) => i !== index)
        }));
    };

    const handleItemChange = (index, field, value) => {
        const newItems = [...formData.items];
        newItems[index][field] = value;
        setFormData(prev => ({ ...prev, items: newItems }));
    };

    const calculateTotal = () => {
        return formData.items.reduce((acc, item) => acc + (item.quantidade * item.valorUnitario), 0);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (viewOnly) return;

        if (formData.items.length === 0) {
            toast.error("Adicione pelo menos um item");
            return;
        }

        setLoading(true);
        try {
            const payload = {
                clienteId: formData.clienteId,
                items: formData.items.map(i => ({
                    nomeProduto: i.nomeProduto,
                    quantidade: Number(i.quantidade),
                    valorUnitario: Number(i.valorUnitario)
                }))
            };

            if (initialData?.id) {
                await updateOrder(api, initialData.id, payload);
                toast.success('Pedido atualizado com sucesso');
            } else {
                await createOrder(api, payload);
                toast.success('Pedido criado com sucesso');
            }
            onSuccess();
            onClose();
        } catch (error) {
            console.error(error);
            toast.error('Erro ao salvar pedido');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Modal
            isOpen={isOpen}
            onClose={onClose}
            title={viewOnly ? 'Visualizar Pedido' : (initialData ? 'Editar Pedido' : 'Novo Pedido')}
            maxWidth="max-w-4xl"
        >
            <form onSubmit={handleSubmit} className="space-y-6">

                <div className="flex flex-col gap-1 w-full max-w-md">
                    <label className="text-sm font-medium text-gray-700">Cliente</label>
                    <select
                        className="px-3 py-2 bg-white border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition-all disabled:bg-gray-100 disabled:text-gray-500"
                        value={formData.clienteId}
                        onChange={(e) => setFormData({ ...formData, clienteId: e.target.value })}
                        required
                        disabled={!!initialData || viewOnly}
                    >
                        <option value="">Selecione um Cliente</option>
                        {clients.filter(client => initialData ? true : client.ativo).map(client => (
                            <option key={client.id} value={client.id}>
                                {client.nome} - {client.cpfCnpj}
                            </option>
                        ))}
                    </select>
                </div>

                <div>
                    <div className="flex items-center justify-between mb-2">
                        <h4 className="font-semibold text-gray-700">Itens do Pedido</h4>
                        {!viewOnly && (
                            <Button type="button" variant="secondary" onClick={handleAddItem} size="sm">
                                <Plus size={16} /> Adicionar Item
                            </Button>
                        )}
                    </div>

                    <div className="space-y-3 bg-gray-50 p-4 rounded-lg border border-gray-200">
                        {formData.items.length === 0 && (
                            <p className="text-sm text-gray-500 text-center py-4">Nenhum item adicionado.</p>
                        )}
                        {formData.items.map((item, index) => (
                            <div key={index} className={`grid ${viewOnly ? 'grid-cols-[3fr_1fr_1.5fr]' : 'grid-cols-[3fr_1fr_1.5fr_auto]'} gap-3 items-end`}>
                                <Input
                                    label={index === 0 ? "Nome do Produto" : ""}
                                    value={item.nomeProduto}
                                    onChange={(e) => handleItemChange(index, "nomeProduto", e.target.value)}
                                    required
                                    disabled={viewOnly}
                                />
                                <Input
                                    label={index === 0 ? "Quantidade" : ""}
                                    type="number"
                                    min="1"
                                    value={item.quantidade}
                                    onChange={(e) => handleItemChange(index, "quantidade", e.target.value)}
                                    required
                                    disabled={viewOnly}
                                />
                                <Input
                                    label={index === 0 ? "Preço Unitário" : ""}
                                    type="number"
                                    step="0.01"
                                    value={item.valorUnitario}
                                    onChange={(e) => handleItemChange(index, "valorUnitario", e.target.value)}
                                    required
                                    disabled={viewOnly}
                                />
                                {!viewOnly && (
                                    <button
                                        type="button"
                                        onClick={() => handleRemoveItem(index)}
                                        className="p-2 text-red-500 hover:bg-red-50 rounded mb-[2px]"
                                        title="Remover item"
                                    >
                                        <Trash2 size={18} />
                                    </button>
                                )}
                            </div>
                        ))}
                    </div>

                    <div className="flex justify-end mt-2">
                        <div className="text-lg font-bold text-gray-900">
                            Total: R$ {calculateTotal().toFixed(2)}
                        </div>
                    </div>
                </div>

                <div className="flex justify-end gap-2 mt-6 border-t pt-4">
                    <Button type="button" variant="secondary" onClick={onClose} disabled={loading}>
                        {viewOnly ? "Fechar" : "Cancelar"}
                    </Button>
                    {!viewOnly && (
                        <Button type="submit" isLoading={loading}>
                            Salvar
                        </Button>
                    )}
                </div>
            </form>
        </Modal>
    );
};

export default OrderForm;
