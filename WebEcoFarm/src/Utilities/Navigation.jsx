export default function useSlug(name) {
    if (!(name?.length > 0))
        return "";
    return name.toLowerCase()
        .trim()
        .replace(/[^a-z0-9]+/g, '-')
        .replace(/^-+|-+$/g, '');
}

