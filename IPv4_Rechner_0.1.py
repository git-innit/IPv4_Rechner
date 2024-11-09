import ipaddress

def ip_rechner():
    # IP-Adresse im CIDR-Format vom Benutzer abfragen
    cidr_eingabe = input("Gib die IP-Adresse im CIDR-Format ein (z.B. 123.128.1.0/17): ")
    
    try:
        # IPv4-Netzwerk erstellen
        netzwerk = ipaddress.IPv4Network(cidr_eingabe, strict=False)
    except ValueError:
        print("Ungültiges CIDR-Format. Bitte eine gültige IP-Adresse im CIDR-Format eingeben.")
        return

    # Benutzerdefinierte IP-Adresse und Subnetzmaske extrahieren
    ip_adresse = str(netzwerk.network_address)
    subnetz_maske = str(netzwerk.netmask)

    # Eingabewert anzeigen
    eingegebene_ip = cidr_eingabe.split('/')[0]

    # Netzwerkadresse
    netzwerk_adresse = netzwerk.network_address

    # Kleinster und größter Host
    hosts = list(netzwerk.hosts())
    kleinster_host = hosts[0] if hosts else "N/A"
    größter_host = hosts[-1] if hosts else "N/A"

    # Broadcast-Adresse
    broadcast_adresse = netzwerk.broadcast_address

    # Anzahl der Hosts
    host_anzahl = netzwerk.num_addresses - 2 if netzwerk.prefixlen < 32 else 1
    host_potenz = f"2^{netzwerk.max_prefixlen - netzwerk.prefixlen}" if host_anzahl > 1 else "N/A"

    # Netzklasse berechnen
    erste_octet = int(ip_adresse.split('.')[0])
    if 1 <= erste_octet <= 126:
        netzklasse = "A"
    elif 128 <= erste_octet <= 191:
        netzklasse = "B"
    elif 192 <= erste_octet <= 223:
        netzklasse = "C"
    elif 224 <= erste_octet <= 239:
        netzklasse = "D (Multicast)"
    else:
        netzklasse = "E (Experimentell)"

    # Ausgabe
    print(f"\nIP Adresse:\t{eingegebene_ip}")
    print(f"Subnetz:\t{subnetz_maske}")
    print()
    print(f"Netzadresse:\t{netzwerk_adresse}")
    print(f"kleinster Host:\t{kleinster_host}")
    print(f"größter Host:\t{größter_host}")
    print(f"Broadcast:\t{broadcast_adresse}")
    print()
    print(f"Anzahl Host:\t{host_anzahl} ({host_potenz})")
    print(f"Netzklasse:\t{netzklasse}")

# Funktion aufrufen
ip_rechner()

input("exit")
