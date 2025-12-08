import clsx from 'clsx';
import { twMerge } from 'tailwind-merge';

const Skeleton = ({ className, ...props }) => {
    return (
        <div
            className={twMerge("animate-pulse bg-gray-200 rounded-md", className)}
            {...props}
        />
    );
};

export default Skeleton;
