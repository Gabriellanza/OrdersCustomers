import { useState, useEffect } from 'react';
import { useAxios } from '../../hooks/useAxios';
import { createClient, updateClient } from '../../services/clientService';
import Modal from '../../components/ui/Modal';
import Input from '../../components/ui/Input';
import Button from '../../components/ui/Button';
import toast from 'react-hot-toast';

const ClientForm = ({ isOpen, onClose, onSuccess, initialData }) => {
    const api = useAxios();
    const [loading, setLoading] = useState(false);

    const initialFormState = {
        cpfCnpj: '',
        nome: '',
        email: '',
        telefone: '',
        celular: '',
        endereco: {
            cep: '',
            logradouro: '',
            numero: '',
            bairro: '',
            cidade: '',
            estado: ''
        }
    };

    const [formData, setFormData] = useState(initialFormState);

    useEffect(() => {
        if (initialData) {
            setFormData({
                cpfCnpj: initialData.cpfCnpj || '',
                nome: initialData.nome || '',
                email: initialData.email || '',
                telefone: initialData.telefone || '',
                celular: initialData.celular || '',
                endereco: {
                    cep: initialData.endereco?.cep || '',
                    logradouro: initialData.endereco?.logradouro || '',
                    numero: initialData.endereco?.numero || '',
                    bairro: initialData.endereco?.bairro || '',
                    cidade: initialData.endereco?.cidade || '',
                    estado: initialData.endereco?.estado || ''
                }
            });
        } else {
            setFormData(initialFormState);
        }
    }, [initialData, isOpen]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleAddressChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            endereco: { ...prev.endereco, [name]: value }
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        try {
            if (initialData?.id) {
                await updateClient(api, initialData.id, formData);
                toast.success('Cliente alterado com sucesso');
            } else {
                await createClient(api, formData);
                toast.success('Cliente criado com sucesso');
            }
            onSuccess();
            onClose();
        } catch (error) {
            console.error(error);
            toast.error('Erro ao salvar cliente');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Modal
            isOpen={isOpen}
            onClose={onClose}
            title={initialData ? 'Editar Cliente' : 'Novo Cliente'}
            maxWidth="max-w-2xl"
        >
            <form onSubmit={handleSubmit} className="space-y-4">
                <h4 className="font-semibold text-gray-700 border-b pb-1">Informações Pessoais</h4>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <Input
                        label="Nome"
                        name="nome"
                        value={formData.nome}
                        onChange={handleChange}
                        required
                    />
                    <Input
                        label="CPF/CNPJ"
                        name="cpfCnpj"
                        value={formData.cpfCnpj}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <Input
                        label="Email"
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                    />
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <Input
                        label="Telefone"
                        name="telefone"
                        value={formData.telefone}
                        onChange={handleChange}
                    />
                    <Input
                        label="Celular"
                        name="celular"
                        value={formData.celular}
                        onChange={handleChange}
                    />
                </div>

                <h4 className="font-semibold text-gray-700 border-b pb-1 mt-4">Endereço</h4>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <Input
                        label="CEP"
                        name="cep"
                        value={formData.endereco.cep}
                        onChange={handleAddressChange}
                        required
                    />
                    <Input
                        label="Estado (UF)"
                        name="estado"
                        value={formData.endereco.estado}
                        onChange={handleAddressChange}
                        required
                        maxLength={2}
                    />
                </div>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <Input
                        label="Cidade"
                        name="cidade"
                        value={formData.endereco.cidade}
                        onChange={handleAddressChange}
                        required
                    />
                    <Input
                        label="Bairro"
                        name="bairro"
                        value={formData.endereco.bairro}
                        onChange={handleAddressChange}
                        required
                    />
                </div>
                <div className="grid grid-cols-1 md:grid-cols-[2fr_1fr] gap-4">
                    <Input
                        label="Rua"
                        name="logradouro"
                        value={formData.endereco.logradouro}
                        onChange={handleAddressChange}
                        required
                    />
                    <Input
                        label="Número"
                        name="numero"
                        value={formData.endereco.numero}
                        onChange={handleAddressChange}
                        required
                    />
                </div>

                <div className="flex justify-end gap-2 mt-6 border-t pt-4">
                    <Button type="button" variant="secondary" onClick={onClose} disabled={loading}>
                        Cancelar
                    </Button>
                    <Button type="submit" isLoading={loading}>
                        Salvar
                    </Button>
                </div>
            </form>
        </Modal>
    );
};

export default ClientForm;
