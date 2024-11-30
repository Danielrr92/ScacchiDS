import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
//import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

//const baseFolder =
//    env.APPDATA !== undefined && env.APPDATA !== ''
//        ? `${env.APPDATA}/ASP.NET/https`
//        : `${env.HOME}/.aspnet/https`;

//const certificateName = "localhost";
//const certFilePath = path.join(baseFolder, `localhost.pem`);
//const keyFilePath = path.join(baseFolder, `localhost-key.pem`);

if (!fs.existsSync('./localhost.pem') || !fs.existsSync('./localhost-key.pem')) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        './localhost.pem',
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7225';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/weatherforecast': {
                target,
                secure: false
            }
        },
        port: 5173,
        https: {
            key: fs.readFileSync('./localhost-key.pem'),
            cert: fs.readFileSync('./localhost.pem'),
        }
    }
})
