/** @type {import('next').NextConfig} */
const nextConfig = {
    env:{
        API_URL:"http://localhost:5023",
    },
    images: {
        remotePatterns: [
            {
                protocol: 'https',
                hostname: '**',
                port: '',
                pathname: '**',
            },
        ],
    },
};

export default nextConfig;
