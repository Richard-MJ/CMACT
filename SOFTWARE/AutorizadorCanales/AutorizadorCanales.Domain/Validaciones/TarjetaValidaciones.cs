using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Excepciones;

namespace AutorizadorCanales.Domain.Validaciones;

public static class TarjetaValidaciones
{
    public static void ValidarTarjetaInicioSesion(this Tarjeta tarjeta, string canalOrigen)
    {
        if (canalOrigen == CanalElectronicoConstante.BANKING_EMPRESARIAL)
        {
            if (tarjeta.EstaAfiliadoHomeBanking())
                throw new ExcepcionAUsuario("06", "Cliente afiliado a Tu Caja por Internet Personas.");
        }
        else
        {
            if (!tarjeta.EstaAfiliadoHomeBanking())
                throw new ExcepcionAUsuario("06", "Cliente no afiliado a los Canales Electrónicos.");
        }

        if (tarjeta.NoRegistraClaveHomeBanking())
            throw new ExcepcionAUsuario("06", "Cliente no afiliado a los Canales Electrónicos.");
    }

    public static void ValidarTarjetaAfiliacion(this Tarjeta tarjeta)
    {
        if (!tarjeta.NoRegistraClaveHomeBanking() && tarjeta.AfiliacionCanalElectronico!.EsTarjetaAfiliada)
            throw new ExcepcionAUsuario("06", "Cliente ya afiliado a los Canales Electrónicos.");
    }
}
